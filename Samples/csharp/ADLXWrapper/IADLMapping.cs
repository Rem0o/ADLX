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

public class IADLMapping : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal IADLMapping(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IADLMapping obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(IADLMapping obj) {
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

  ~IADLMapping() {
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
          ADLXPINVOKE.delete_IADLMapping(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public virtual ADLX_RESULT GetADLXGPUFromBdf(int bus, int device, int function, SWIGTYPE_p_p_adlx__IADLXGPU ppGPU) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLMapping_GetADLXGPUFromBdf(swigCPtr, bus, device, function, SWIGTYPE_p_p_adlx__IADLXGPU.getCPtr(ppGPU));
    return ret;
  }

  public virtual ADLX_RESULT GetADLXGPUFromAdlAdapterIndex(int adlAdapterIndex, SWIGTYPE_p_p_adlx__IADLXGPU ppGPU) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLMapping_GetADLXGPUFromAdlAdapterIndex(swigCPtr, adlAdapterIndex, SWIGTYPE_p_p_adlx__IADLXGPU.getCPtr(ppGPU));
    return ret;
  }

  public virtual ADLX_RESULT BdfFromADLXGPU(IADLXGPU pGPU, SWIGTYPE_p_int bus, SWIGTYPE_p_int device, SWIGTYPE_p_int function) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLMapping_BdfFromADLXGPU(swigCPtr, IADLXGPU.getCPtr(pGPU), SWIGTYPE_p_int.getCPtr(bus), SWIGTYPE_p_int.getCPtr(device), SWIGTYPE_p_int.getCPtr(function));
    return ret;
  }

  public virtual ADLX_RESULT AdlAdapterIndexFromADLXGPU(IADLXGPU pGPU, SWIGTYPE_p_int adlAdapterIndex) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLMapping_AdlAdapterIndexFromADLXGPU(swigCPtr, IADLXGPU.getCPtr(pGPU), SWIGTYPE_p_int.getCPtr(adlAdapterIndex));
    return ret;
  }

  public virtual ADLX_RESULT GetADLXDisplayFromADLIds(int adapterIndex, int displayIndex, int bus, int device, int function, SWIGTYPE_p_p_adlx__IADLXDisplay ppDisplay) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLMapping_GetADLXDisplayFromADLIds(swigCPtr, adapterIndex, displayIndex, bus, device, function, SWIGTYPE_p_p_adlx__IADLXDisplay.getCPtr(ppDisplay));
    return ret;
  }

  public virtual ADLX_RESULT ADLIdsFromADLXDisplay(IADLXDisplay pDisplay, SWIGTYPE_p_int adapterIndex, SWIGTYPE_p_int displayIndex, SWIGTYPE_p_int bus, SWIGTYPE_p_int device, SWIGTYPE_p_int function) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLMapping_ADLIdsFromADLXDisplay(swigCPtr, IADLXDisplay.getCPtr(pDisplay), SWIGTYPE_p_int.getCPtr(adapterIndex), SWIGTYPE_p_int.getCPtr(displayIndex), SWIGTYPE_p_int.getCPtr(bus), SWIGTYPE_p_int.getCPtr(device), SWIGTYPE_p_int.getCPtr(function));
    return ret;
  }

  public virtual ADLX_RESULT GetADLXDesktopFromADLIds(int adapterIndex, int VidPnSourceId, int bus, int device, int function, SWIGTYPE_p_p_adlx__IADLXDesktop ppDesktop) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLMapping_GetADLXDesktopFromADLIds(swigCPtr, adapterIndex, VidPnSourceId, bus, device, function, SWIGTYPE_p_p_adlx__IADLXDesktop.getCPtr(ppDesktop));
    return ret;
  }

  public virtual ADLX_RESULT ADLIdsFromADLXDesktop(SWIGTYPE_p_adlx__IADLXDesktop pDesktop, SWIGTYPE_p_int adapterIndex, SWIGTYPE_p_int VidPnSourceId, SWIGTYPE_p_int bus, SWIGTYPE_p_int device, SWIGTYPE_p_int function) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLMapping_ADLIdsFromADLXDesktop(swigCPtr, SWIGTYPE_p_adlx__IADLXDesktop.getCPtr(pDesktop), SWIGTYPE_p_int.getCPtr(adapterIndex), SWIGTYPE_p_int.getCPtr(VidPnSourceId), SWIGTYPE_p_int.getCPtr(bus), SWIGTYPE_p_int.getCPtr(device), SWIGTYPE_p_int.getCPtr(function));
    return ret;
  }

}

}
