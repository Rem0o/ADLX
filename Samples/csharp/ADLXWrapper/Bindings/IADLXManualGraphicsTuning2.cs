//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.1.1
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace ADLXWrapper.Bindings {

public class IADLXManualGraphicsTuning2 : IADLXInterface {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal IADLXManualGraphicsTuning2(global::System.IntPtr cPtr, bool cMemoryOwn) : base(ADLXPINVOKE.IADLXManualGraphicsTuning2_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IADLXManualGraphicsTuning2 obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(IADLXManualGraphicsTuning2 obj) {
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

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          ADLXPINVOKE.delete_IADLXManualGraphicsTuning2(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public new static SWIGTYPE_p_wchar_t IID() {
    global::System.IntPtr cPtr = ADLXPINVOKE.IADLXManualGraphicsTuning2_IID();
    SWIGTYPE_p_wchar_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_wchar_t(cPtr, false);
    return ret;
  }

  public virtual ADLX_RESULT GetGPUMinFrequencyRange(ADLX_IntRange tuningRange) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualGraphicsTuning2_GetGPUMinFrequencyRange(swigCPtr, ADLX_IntRange.getCPtr(tuningRange));
    return ret;
  }

  public virtual ADLX_RESULT GetGPUMinFrequency(SWIGTYPE_p_int minFreq) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualGraphicsTuning2_GetGPUMinFrequency(swigCPtr, SWIGTYPE_p_int.getCPtr(minFreq));
    return ret;
  }

  public virtual ADLX_RESULT SetGPUMinFrequency(int minFreq) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualGraphicsTuning2_SetGPUMinFrequency(swigCPtr, minFreq);
    return ret;
  }

  public virtual ADLX_RESULT GetGPUMaxFrequencyRange(ADLX_IntRange tuningRange) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualGraphicsTuning2_GetGPUMaxFrequencyRange(swigCPtr, ADLX_IntRange.getCPtr(tuningRange));
    return ret;
  }

  public virtual ADLX_RESULT GetGPUMaxFrequency(SWIGTYPE_p_int maxFreq) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualGraphicsTuning2_GetGPUMaxFrequency(swigCPtr, SWIGTYPE_p_int.getCPtr(maxFreq));
    return ret;
  }

  public virtual ADLX_RESULT SetGPUMaxFrequency(int maxFreq) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualGraphicsTuning2_SetGPUMaxFrequency(swigCPtr, maxFreq);
    return ret;
  }

  public virtual ADLX_RESULT GetGPUVoltageRange(ADLX_IntRange tuningRange) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualGraphicsTuning2_GetGPUVoltageRange(swigCPtr, ADLX_IntRange.getCPtr(tuningRange));
    return ret;
  }

  public virtual ADLX_RESULT GetGPUVoltage(SWIGTYPE_p_int volt) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualGraphicsTuning2_GetGPUVoltage(swigCPtr, SWIGTYPE_p_int.getCPtr(volt));
    return ret;
  }

  public virtual ADLX_RESULT SetGPUVoltage(int volt) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualGraphicsTuning2_SetGPUVoltage(swigCPtr, volt);
    return ret;
  }

}

}
