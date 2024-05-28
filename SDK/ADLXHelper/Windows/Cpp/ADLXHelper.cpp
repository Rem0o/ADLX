//
// Copyright (c) 2021 - 2023 Advanced Micro Devices, Inc. All rights reserved.
//
//-------------------------------------------------------------------------------------------------

#include "ADLXHelper.h"
#include "../../../Include/IGPUManualFanTuning.h"
#include "../../../Include/IPerformanceMonitoring.h"
using namespace adlx;

ADLXHelper g_ADLX;

//-------------------------------------------------------------------------------------------------
//Constructor
ADLXHelper::ADLXHelper()
{
	m_metricsPtr = new adlx::IADLXGPUMetrics * ();
	m_metricsListPtr = new adlx::IADLXGPUMetricsList * ();
}

//-------------------------------------------------------------------------------------------------
//Destructor
ADLXHelper::~ADLXHelper()
{
	Terminate();
}

//-------------------------------------------------------------------------------------------------
//Initialization
ADLX_RESULT ADLXHelper::Initialize()
{
	return InitializePrivate(nullptr, nullptr);
}

ADLX_RESULT ADLXHelper::InitializeWithIncompatibleDriver()
{
	return InitializePrivate(nullptr, nullptr, true);
}

ADLX_RESULT ADLXHelper::InitializeWithCallerAdl(adlx_handle adlContext, ADLX_ADL_Main_Memory_Free adlMainMemoryFree)
{
	if (adlContext == nullptr || adlMainMemoryFree == nullptr)
	{
		return ADLX_INVALID_ARGS;
	}
	return InitializePrivate(adlContext, adlMainMemoryFree);
}

//-------------------------------------------------------------------------------------------------
//Termination
ADLX_RESULT ADLXHelper::Terminate()
{
	ADLX_RESULT res = ADLX_OK;
	if (m_hDLLHandle != nullptr)
	{
		delete m_metricsPtr;
		delete m_metricsListPtr;
		m_metricsListPtr = nullptr;
		m_metricsPtr = nullptr;

		m_ADLXFullVersion = 0;
		m_ADLXVersion = nullptr;
		m_pSystemServices = nullptr;
		m_pAdlMapping = nullptr;
		if (nullptr != m_terminateFn)
		{
			res = m_terminateFn();
		}
		m_fullVersionFn = nullptr;
		m_versionFn = nullptr;
		m_initWithADLFn = nullptr;
		m_initFnEx = nullptr;
		m_initFn = nullptr;
		m_terminateFn = nullptr;
		adlx_free_library(m_hDLLHandle);
		m_hDLLHandle = nullptr;
		oneState = nullptr;
	}
	return res;
}

//-------------------------------------------------------------------------------------------------
//Gets the full version of ADLX
adlx_uint64 ADLXHelper::QueryFullVersion()
{
	return m_ADLXFullVersion;
}

//-------------------------------------------------------------------------------------------------
//Gets the version of ADLX
const char* ADLXHelper::QueryVersion()
{
	return m_ADLXVersion;
}

//-------------------------------------------------------------------------------------------------
//Gets the ADLX system interface
adlx::IADLXSystem* ADLXHelper::GetSystemServices()
{
	return m_pSystemServices;
}

//-------------------------------------------------------------------------------------------------
//Gets the ADL Mapping interface
adlx::IADLMapping* ADLXHelper::GetAdlMapping()
{
	return m_pAdlMapping;
}

ADLX_RESULT ADLXHelper::SetSpeed(IADLXManualFanTuning* fanTuning, int speed, adlx::IADLXManualFanTuningStateList* list) {

	ADLX_RESULT res;
	for (unsigned int i = list->Begin(); i < list->End(); i++) 
	{
		res = list->At(i, &oneState);
		if (!ADLX_SUCCEEDED(res)) {
			return res;
		}

		res = oneState->SetFanSpeed(speed);
		oneState->Release();

		if (!ADLX_SUCCEEDED(res)) {
			return res;
		}
	}

	return fanTuning->SetFanTuningStates(list);
}

ADLX_RESULT ADLXHelper::GetCurrentMetrics(adlx::IADLXPerformanceMonitoringServices* services, adlx::IADLXGPU* gpu, GPUMetricsStruct* metrics) {

	ADLX_RESULT res = ADLX_OK;

	res = services->GetCurrentGPUMetrics(gpu, m_metricsPtr);
	if (!ADLX_SUCCEEDED(res))
	{
		return res;
	}

	IADLXGPUMetrics* current = *m_metricsPtr;

	res = current->GPUFanSpeed(&metrics->GPUFanSpeed);
	res = current->GPUHotspotTemperature(&metrics->GPUHotspotTemperature);
	res = current->GPUTemperature(&metrics->GPUTemperature);

	current->Release();

	return res;
}

ADLX_RESULT ADLXHelper::GetLatestMetricsFromTracking(adlx::IADLXPerformanceMonitoringServices* services, adlx::IADLXGPU* gpu, GPUMetricsStruct* metrics)
{
	bool needFallback = true;

	ADLX_RESULT res = services->GetGPUMetricsHistory(gpu, 1, 0, m_metricsListPtr);

	if (res == ADLX_OK)
	{
		adlx::IADLXGPUMetricsList* list = (*m_metricsListPtr);
		if (list->Size() > 0)
		{
			res = list->At(0, m_metricsPtr);
			if (res == ADLX_OK)
			{
				needFallback = false;
			}
		}

		list->Release();
	}

	if (needFallback)
	{
		res = services->GetCurrentGPUMetrics(gpu, m_metricsPtr);
	}

	if (res == ADLX_OK)
	{
		IADLXGPUMetrics* current = *m_metricsPtr;
		res = current->GPUFanSpeed(&metrics->GPUFanSpeed);
		res = current->GPUHotspotTemperature(&metrics->GPUHotspotTemperature);
		res = current->GPUTemperature(&metrics->GPUTemperature);
		current->Release();
	}

	return res;
}


//-------------------------------------------------------------------------------------------------
//Loads ADLX and finds the function pointers to the ADLX functions
ADLX_RESULT ADLXHelper::LoadADLXDll()
{
	if (m_hDLLHandle == nullptr)
	{
		m_hDLLHandle = adlx_load_library(ADLX_DLL_NAME);
		if (m_hDLLHandle)
		{
			m_fullVersionFn = (ADLXQueryFullVersion_Fn)adlx_get_proc_address(m_hDLLHandle, ADLX_QUERY_FULL_VERSION_FUNCTION_NAME);
			m_versionFn = (ADLXQueryVersion_Fn)adlx_get_proc_address(m_hDLLHandle, ADLX_QUERY_VERSION_FUNCTION_NAME);
			m_initWithADLFn = (ADLXInitializeWithCallerAdl_Fn)adlx_get_proc_address(m_hDLLHandle, ADLX_INIT_WITH_CALLER_ADL_FUNCTION_NAME);
			m_initFnEx = (ADLXInitialize_Fn)adlx_get_proc_address(m_hDLLHandle, ADLX_INIT_WITH_INCOMPATIBLE_DRIVER_FUNCTION_NAME);
			m_initFn = (ADLXInitialize_Fn)adlx_get_proc_address(m_hDLLHandle, ADLX_INIT_FUNCTION_NAME);
			m_terminateFn = (ADLXTerminate_Fn)adlx_get_proc_address(m_hDLLHandle, ADLX_TERMINATE_FUNCTION_NAME);
		}
	}

	if (m_fullVersionFn && m_versionFn && m_initWithADLFn && m_initFnEx && m_initFn && m_terminateFn)
	{
		return ADLX_OK;
	}

	return ADLX_FAIL;
}

//-------------------------------------------------------------------------------------------------
//Initializes ADLX based on the  parameters
ADLX_RESULT ADLXHelper::InitializePrivate(adlx_handle  adlContext, ADLX_ADL_Main_Memory_Free adlMainMemoryFree, adlx_bool useIncompatibleDriver)
{
	ADLX_RESULT res = LoadADLXDll();
	if (ADLX_OK == res)
	{
		m_fullVersionFn(&m_ADLXFullVersion);
		m_versionFn(&m_ADLXVersion);
		if (adlContext != nullptr && adlMainMemoryFree != nullptr)
		{
			res = m_initWithADLFn(ADLX_FULL_VERSION, &m_pSystemServices, &m_pAdlMapping, adlContext, adlMainMemoryFree);
		}
		else
		{
			if (useIncompatibleDriver)
			{
				res = m_initFnEx(ADLX_FULL_VERSION, &m_pSystemServices);
			}
			else
			{
				res = m_initFn(ADLX_FULL_VERSION, &m_pSystemServices);
			}
		}
		return res;
	}

	return ADLX_FAIL;
}