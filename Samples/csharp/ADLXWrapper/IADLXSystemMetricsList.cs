//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.1.1
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace ADLXWrapper {

public class IADLXSystemMetricsList : IADLXList {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal IADLXSystemMetricsList(global::System.IntPtr cPtr, bool cMemoryOwn) : base(ADLXPINVOKE.IADLXSystemMetricsList_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IADLXSystemMetricsList obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(IADLXSystemMetricsList obj) {
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
          ADLXPINVOKE.delete_IADLXSystemMetricsList(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public new static SWIGTYPE_p_wchar_t IID() {
    global::System.IntPtr cPtr = ADLXPINVOKE.IADLXSystemMetricsList_IID();
    SWIGTYPE_p_wchar_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_wchar_t(cPtr, false);
    return ret;
  }

  public new static SWIGTYPE_p_wchar_t ITEM_IID() {
    global::System.IntPtr cPtr = ADLXPINVOKE.IADLXSystemMetricsList_ITEM_IID();
    SWIGTYPE_p_wchar_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_wchar_t(cPtr, false);
    return ret;
  }

  public virtual ADLX_RESULT At(uint location, SWIGTYPE_p_p_adlx__IADLXSystemMetrics ppItem) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXSystemMetricsList_At(swigCPtr, location, SWIGTYPE_p_p_adlx__IADLXSystemMetrics.getCPtr(ppItem));
    return ret;
  }

  public virtual ADLX_RESULT Add_Back(IADLXSystemMetrics pItem) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXSystemMetricsList_Add_Back(swigCPtr, IADLXSystemMetrics.getCPtr(pItem));
    return ret;
  }

}

}
