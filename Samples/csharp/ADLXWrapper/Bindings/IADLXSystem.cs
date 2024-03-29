//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.2.1
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace ADLXWrapper.Bindings {

public class IADLXSystem : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal IADLXSystem(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IADLXSystem obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(IADLXSystem obj) {
    if (obj != null) {
      if (!obj.swigCMemOwn)
        throw new global::System.ApplicationException("Cannot release ownership as memory is not owned");
      global::System.Runtime.InteropServices.HandleRef ptr = obj.swigCPtr;
      obj.swigCMemOwn = false;
      obj.Dispose();
      return ptr;
    } else {
      return new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
    }
  }

  ~IADLXSystem() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          ADLXPINVOKE.delete_IADLXSystem(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public virtual ADLX_RESULT HybridGraphicsType(SWIGTYPE_p_ADLX_HG_TYPE hgType) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXSystem_HybridGraphicsType(swigCPtr, SWIGTYPE_p_ADLX_HG_TYPE.getCPtr(hgType));
    return ret;
  }

  public virtual ADLX_RESULT GetGPUs(SWIGTYPE_p_p_adlx__IADLXGPUList ppGPUs) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXSystem_GetGPUs(swigCPtr, SWIGTYPE_p_p_adlx__IADLXGPUList.getCPtr(ppGPUs));
    return ret;
  }

  public virtual ADLX_RESULT QueryInterface(SWIGTYPE_p_wchar_t interfaceId, SWIGTYPE_p_p_void ppInterface) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXSystem_QueryInterface(swigCPtr, SWIGTYPE_p_wchar_t.getCPtr(interfaceId), SWIGTYPE_p_p_void.getCPtr(ppInterface));
    return ret;
  }

  public virtual ADLX_RESULT GetDisplaysServices(SWIGTYPE_p_p_adlx__IADLXDisplayServices ppDispServices) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXSystem_GetDisplaysServices(swigCPtr, SWIGTYPE_p_p_adlx__IADLXDisplayServices.getCPtr(ppDispServices));
    return ret;
  }

  public virtual ADLX_RESULT GetDesktopsServices(SWIGTYPE_p_p_adlx__IADLXDesktopServices ppDeskServices) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXSystem_GetDesktopsServices(swigCPtr, SWIGTYPE_p_p_adlx__IADLXDesktopServices.getCPtr(ppDeskServices));
    return ret;
  }

  public virtual ADLX_RESULT GetGPUsChangedHandling(SWIGTYPE_p_p_adlx__IADLXGPUsChangedHandling ppGPUsChangedHandling) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXSystem_GetGPUsChangedHandling(swigCPtr, SWIGTYPE_p_p_adlx__IADLXGPUsChangedHandling.getCPtr(ppGPUsChangedHandling));
    return ret;
  }

  public virtual ADLX_RESULT EnableLog(ADLX_LOG_DESTINATION mode, ADLX_LOG_SEVERITY severity, IADLXLog pLogger, SWIGTYPE_p_wchar_t fileName) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXSystem_EnableLog(swigCPtr, (int)mode, (int)severity, IADLXLog.getCPtr(pLogger), SWIGTYPE_p_wchar_t.getCPtr(fileName));
    return ret;
  }

  public virtual ADLX_RESULT Get3DSettingsServices(SWIGTYPE_p_p_adlx__IADLX3DSettingsServices pp3DSettingsServices) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXSystem_Get3DSettingsServices(swigCPtr, SWIGTYPE_p_p_adlx__IADLX3DSettingsServices.getCPtr(pp3DSettingsServices));
    return ret;
  }

  public virtual ADLX_RESULT GetGPUTuningServices(SWIGTYPE_p_p_adlx__IADLXGPUTuningServices ppGPUTuningServices) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXSystem_GetGPUTuningServices(swigCPtr, SWIGTYPE_p_p_adlx__IADLXGPUTuningServices.getCPtr(ppGPUTuningServices));
    return ret;
  }

  public virtual ADLX_RESULT GetPerformanceMonitoringServices(SWIGTYPE_p_p_adlx__IADLXPerformanceMonitoringServices ppPerformanceMonitoringServices) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXSystem_GetPerformanceMonitoringServices(swigCPtr, SWIGTYPE_p_p_adlx__IADLXPerformanceMonitoringServices.getCPtr(ppPerformanceMonitoringServices));
    return ret;
  }

  public virtual ADLX_RESULT TotalSystemRAM(SWIGTYPE_p_unsigned_int ramMB) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXSystem_TotalSystemRAM(swigCPtr, SWIGTYPE_p_unsigned_int.getCPtr(ramMB));
    return ret;
  }

  public virtual ADLX_RESULT GetI2C(IADLXGPU pGPU, SWIGTYPE_p_p_adlx__IADLXI2C ppI2C) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXSystem_GetI2C(swigCPtr, IADLXGPU.getCPtr(pGPU), SWIGTYPE_p_p_adlx__IADLXI2C.getCPtr(ppI2C));
    return ret;
  }

}

}
