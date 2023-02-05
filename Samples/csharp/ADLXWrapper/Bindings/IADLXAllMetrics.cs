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

public class IADLXAllMetrics : IADLXInterface {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal IADLXAllMetrics(global::System.IntPtr cPtr, bool cMemoryOwn) : base(ADLXPINVOKE.IADLXAllMetrics_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IADLXAllMetrics obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(IADLXAllMetrics obj) {
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
          ADLXPINVOKE.delete_IADLXAllMetrics(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public new static SWIGTYPE_p_wchar_t IID() {
    global::System.IntPtr cPtr = ADLXPINVOKE.IADLXAllMetrics_IID();
    SWIGTYPE_p_wchar_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_wchar_t(cPtr, false);
    return ret;
  }

  public virtual ADLX_RESULT TimeStamp(SWIGTYPE_p_long_long ms) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXAllMetrics_TimeStamp(swigCPtr, SWIGTYPE_p_long_long.getCPtr(ms));
    return ret;
  }

  public virtual ADLX_RESULT GetSystemMetrics(SWIGTYPE_p_p_adlx__IADLXSystemMetrics ppSystemMetrics) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXAllMetrics_GetSystemMetrics(swigCPtr, SWIGTYPE_p_p_adlx__IADLXSystemMetrics.getCPtr(ppSystemMetrics));
    return ret;
  }

  public virtual ADLX_RESULT GetFPS(SWIGTYPE_p_p_adlx__IADLXFPS ppFPS) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXAllMetrics_GetFPS(swigCPtr, SWIGTYPE_p_p_adlx__IADLXFPS.getCPtr(ppFPS));
    return ret;
  }

  public virtual ADLX_RESULT GetGPUMetrics(IADLXGPU pGPU, SWIGTYPE_p_p_adlx__IADLXGPUMetrics ppGPUMetrics) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXAllMetrics_GetGPUMetrics(swigCPtr, IADLXGPU.getCPtr(pGPU), SWIGTYPE_p_p_adlx__IADLXGPUMetrics.getCPtr(ppGPUMetrics));
    return ret;
  }

}

}