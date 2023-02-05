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

public class ADLX_GamutColorSpace : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal ADLX_GamutColorSpace(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ADLX_GamutColorSpace obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(ADLX_GamutColorSpace obj) {
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

  ~ADLX_GamutColorSpace() {
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
          ADLXPINVOKE.delete_ADLX_GamutColorSpace(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public ADLX_Point red {
    set {
      ADLXPINVOKE.ADLX_GamutColorSpace_red_set(swigCPtr, ADLX_Point.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = ADLXPINVOKE.ADLX_GamutColorSpace_red_get(swigCPtr);
      ADLX_Point ret = (cPtr == global::System.IntPtr.Zero) ? null : new ADLX_Point(cPtr, false);
      return ret;
    } 
  }

  public ADLX_Point green {
    set {
      ADLXPINVOKE.ADLX_GamutColorSpace_green_set(swigCPtr, ADLX_Point.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = ADLXPINVOKE.ADLX_GamutColorSpace_green_get(swigCPtr);
      ADLX_Point ret = (cPtr == global::System.IntPtr.Zero) ? null : new ADLX_Point(cPtr, false);
      return ret;
    } 
  }

  public ADLX_Point blue {
    set {
      ADLXPINVOKE.ADLX_GamutColorSpace_blue_set(swigCPtr, ADLX_Point.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = ADLXPINVOKE.ADLX_GamutColorSpace_blue_get(swigCPtr);
      ADLX_Point ret = (cPtr == global::System.IntPtr.Zero) ? null : new ADLX_Point(cPtr, false);
      return ret;
    } 
  }

  public ADLX_GamutColorSpace() : this(ADLXPINVOKE.new_ADLX_GamutColorSpace(), true) {
  }

}

}