#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>


template <typename R>
struct VirtualFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename T1, typename T2>
struct InvokerActionInvoker2;
template <typename T1, typename T2>
struct InvokerActionInvoker2<T1, T2*>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1 p1, T2* p2)
	{
		void* params[2] = { &p1, p2 };
		method->invoker_method(methodPtr, method, obj, params, NULL);
	}
};
template <typename T1, typename T2, typename T3>
struct InvokerActionInvoker3;
template <typename T1, typename T2, typename T3>
struct InvokerActionInvoker3<T1, T2*, T3*>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1 p1, T2* p2, T3* p3)
	{
		void* params[3] = { &p1, p2, p3 };
		method->invoker_method(methodPtr, method, obj, params, NULL);
	}
};
template <typename T1, typename T2, typename T3>
struct InvokerActionInvoker3<T1*, T2, T3*>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1, T2 p2, T3* p3)
	{
		void* params[3] = { p1, &p2, p3 };
		method->invoker_method(methodPtr, method, obj, params, NULL);
	}
};
template <typename T1, typename T2, typename T3, typename T4>
struct InvokerActionInvoker4;
template <typename T1, typename T2, typename T3, typename T4>
struct InvokerActionInvoker4<T1*, T2, T3*, T4*>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1, T2 p2, T3* p3, T4* p4)
	{
		void* params[4] = { p1, &p2, p3, p4 };
		method->invoker_method(methodPtr, method, obj, params, NULL);
	}
};

// System.Byte[]
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
// System.Delegate[]
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
// System.IntPtr[]
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
// System.Object[]
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
// System.Diagnostics.StackTrace[]
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
// System.Type[]
struct TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB;
// TheBackend.ToolKit.GoogleLogin.Android
struct Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0;
// UnityEngine.AndroidJavaClass
struct AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03;
// UnityEngine.AndroidJavaObject
struct AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0;
// UnityEngine.AndroidJavaProxy
struct AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D;
// UnityEngine.AndroidJavaRunnable
struct AndroidJavaRunnable_tF23B9BBDA8C99A48BCEEA6335A47DA3C0EF34A7F;
// System.Reflection.AssemblyName
struct AssemblyName_t555F1570F523D87D970C6E7F27B1B44C83EADDD2;
// System.AsyncCallback
struct AsyncCallback_t7FEF460CBDCFB9C5FA2EF776984778B9A4145F4C;
// System.Reflection.Binder
struct Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235;
// System.Globalization.CultureInfo
struct CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0;
// System.Delegate
struct Delegate_t;
// System.DelegateData
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
// System.Exception
struct Exception_t;
// UnityEngine.GlobalJavaObjectRef
struct GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8;
// System.IAsyncResult
struct IAsyncResult_t7B9B5A0ECB35DCEC31B8A8122C37D687369253B5;
// System.Collections.IDictionary
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
// System.Reflection.MemberFilter
struct MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553;
// System.Reflection.MethodInfo
struct MethodInfo_t;
// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C;
// System.Runtime.Serialization.SafeSerializationManager
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
// System.String
struct String_t;
// System.Reflection.StrongNameKeyPair
struct StrongNameKeyPair_t0657447B6CFAA8FE880A228AA578EC20BC6AF8F2;
// System.Type
struct Type_t;
// System.Version
struct Version_tE426DB5655D0F22920AE16A2AA9AB7781B8255A7;
// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
// TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback
struct BackendOnUnityCallback_tD10793DE59BC64A9A265FD049418D7CB3CD04362;
// TheBackend.ToolKit.GoogleLogin.Android/GoogleLoginCallback
struct GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9;
// TheBackend.ToolKit.GoogleLogin.Android/GoogleSignOutCallback
struct GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2;
// TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback/<>c__DisplayClass1_0
struct U3CU3Ec__DisplayClass1_0_tAEF0BA6A2A4D4C4291257AAECFC08872DE437A39;
// TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback/<>c__DisplayClass2_0
struct U3CU3Ec__DisplayClass2_0_t34096128F462743F4ECF24F641BC27432EC60C01;

IL2CPP_EXTERN_C RuntimeClass* AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* AndroidJavaRunnable_tF23B9BBDA8C99A48BCEEA6335A47DA3C0EF34A7F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* BackendOnUnityCallback_tD10793DE59BC64A9A265FD049418D7CB3CD04362_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Exception_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* RuntimeObject_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* RuntimePlatform_t9A8AAF204603076FCAAECCCC05DA386AEE7BF66E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* String_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* TheBackendGoogleSettingsForAndroid_t6A3D1A2835CFF801BB7FC6B06490CD842A278F47_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Type_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass1_0_tAEF0BA6A2A4D4C4291257AAECFC08872DE437A39_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass2_0_t34096128F462743F4ECF24F641BC27432EC60C01_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral03272EFE2FAACA7C9ECAA4C85414177AF5115A02;
IL2CPP_EXTERN_C String_t* _stringLiteral2063737B07B6658BC2E1EC3128D4E09E57CA123E;
IL2CPP_EXTERN_C String_t* _stringLiteral3BC29E947F18FE016F459842215F6335AB7D9610;
IL2CPP_EXTERN_C String_t* _stringLiteral42CD92DB2E10FD2A1DDC98FE055DC8D4CCCFDB06;
IL2CPP_EXTERN_C String_t* _stringLiteral4D31205BF9E55201691DD5151AE8015105AF3477;
IL2CPP_EXTERN_C String_t* _stringLiteral4D613657609485AE586A3379BA0E3FC13C1E1078;
IL2CPP_EXTERN_C String_t* _stringLiteral5085336E5FDBAF02EB48DC1A34A9A1636E1677FB;
IL2CPP_EXTERN_C String_t* _stringLiteral5C85C813951C1B718C305DF0F28FF52E4C670727;
IL2CPP_EXTERN_C String_t* _stringLiteral774691F5E0813830196176DBCD276819181055E0;
IL2CPP_EXTERN_C String_t* _stringLiteral7D5D6BBF8281151C9F5F57DE5D5BABB7140A651D;
IL2CPP_EXTERN_C String_t* _stringLiteralAE97D0ED1749ECB596B76299EE34E3F2291ED681;
IL2CPP_EXTERN_C String_t* _stringLiteralB4D25E0E5E2D132E156F790B434A50D5CB83E8EE;
IL2CPP_EXTERN_C String_t* _stringLiteralDF61F088F4F72BC95F76E46C54EDD133EEF6C597;
IL2CPP_EXTERN_C String_t* _stringLiteralFB4AE4F77150C3A8E8E4F8B23E734E0C7277B7D9;
IL2CPP_EXTERN_C const RuntimeMethod* AndroidJavaObject_CallStatic_TisAndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_m398EA96C1DE1BB885F2B1DD0E00E8BBA86B49E63_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AndroidJavaObject_GetStatic_TisAndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_mD7D192A35EB2B2DA3775FAB081958B72088251DD_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AndroidJavaObject_SetStatic_TisAndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_m52D5B3006350B7FB0EAE6B2E2A37EDD1606F01D1_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Android_GoogleLogin_mCBCB7FE461475F404B56E68B1563A50FF843116E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Android_GoogleLogin_mFCCA40B96FD043C48036C73245D57BF73038E903_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Android_GoogleSignOut_m3CF767DFF1D93908801CDC2FF53449F6DEC520CD_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass1_0_U3ConGetAccessTokenU3Eb__0_mBFB387473369A2E1E04E453E8950D20523BB1CC6_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass2_0_U3ConGoogleSignOutU3Eb__0_m001A76F7B26BC0A5D0BADFF0B3B02F8808E45B8A_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeType* Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_0_0_0_var;
IL2CPP_EXTERN_C const RuntimeType* TheBackendGoogleSettingsForAndroid_t6A3D1A2835CFF801BB7FC6B06490CD842A278F47_0_0_0_var;
struct CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_marshaled_com;
struct CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_marshaled_pinvoke;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;

struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// <Module>
struct U3CModuleU3E_t75830D28638B181B672B673809DCC81DA1C8042C 
{
};

// System.EmptyArray`1<System.Object>
struct EmptyArray_1_tDF0DD7256B115243AA6BD5558417387A734240EE  : public RuntimeObject
{
};

// TheBackend.ToolKit.GoogleLogin.Android
struct Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0  : public RuntimeObject
{
};

// UnityEngine.AndroidJavaObject
struct AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0  : public RuntimeObject
{
	// UnityEngine.GlobalJavaObjectRef UnityEngine.AndroidJavaObject::m_jobject
	GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* ___m_jobject_1;
	// UnityEngine.GlobalJavaObjectRef UnityEngine.AndroidJavaObject::m_jclass
	GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* ___m_jclass_2;
};

// System.Reflection.Assembly
struct Assembly_t  : public RuntimeObject
{
};
// Native definition for P/Invoke marshalling of System.Reflection.Assembly
struct Assembly_t_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.Reflection.Assembly
struct Assembly_t_marshaled_com
{
};

// System.Reflection.AssemblyName
struct AssemblyName_t555F1570F523D87D970C6E7F27B1B44C83EADDD2  : public RuntimeObject
{
	// System.String System.Reflection.AssemblyName::name
	String_t* ___name_0;
	// System.String System.Reflection.AssemblyName::codebase
	String_t* ___codebase_1;
	// System.Int32 System.Reflection.AssemblyName::major
	int32_t ___major_2;
	// System.Int32 System.Reflection.AssemblyName::minor
	int32_t ___minor_3;
	// System.Int32 System.Reflection.AssemblyName::build
	int32_t ___build_4;
	// System.Int32 System.Reflection.AssemblyName::revision
	int32_t ___revision_5;
	// System.Globalization.CultureInfo System.Reflection.AssemblyName::cultureinfo
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0* ___cultureinfo_6;
	// System.Reflection.AssemblyNameFlags System.Reflection.AssemblyName::flags
	int32_t ___flags_7;
	// System.Configuration.Assemblies.AssemblyHashAlgorithm System.Reflection.AssemblyName::hashalg
	int32_t ___hashalg_8;
	// System.Reflection.StrongNameKeyPair System.Reflection.AssemblyName::keypair
	StrongNameKeyPair_t0657447B6CFAA8FE880A228AA578EC20BC6AF8F2* ___keypair_9;
	// System.Byte[] System.Reflection.AssemblyName::publicKey
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___publicKey_10;
	// System.Byte[] System.Reflection.AssemblyName::keyToken
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___keyToken_11;
	// System.Configuration.Assemblies.AssemblyVersionCompatibility System.Reflection.AssemblyName::versioncompat
	int32_t ___versioncompat_12;
	// System.Version System.Reflection.AssemblyName::version
	Version_tE426DB5655D0F22920AE16A2AA9AB7781B8255A7* ___version_13;
	// System.Reflection.ProcessorArchitecture System.Reflection.AssemblyName::processor_architecture
	int32_t ___processor_architecture_14;
	// System.Reflection.AssemblyContentType System.Reflection.AssemblyName::contentType
	int32_t ___contentType_15;
};
// Native definition for P/Invoke marshalling of System.Reflection.AssemblyName
struct AssemblyName_t555F1570F523D87D970C6E7F27B1B44C83EADDD2_marshaled_pinvoke
{
	char* ___name_0;
	char* ___codebase_1;
	int32_t ___major_2;
	int32_t ___minor_3;
	int32_t ___build_4;
	int32_t ___revision_5;
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_marshaled_pinvoke* ___cultureinfo_6;
	int32_t ___flags_7;
	int32_t ___hashalg_8;
	StrongNameKeyPair_t0657447B6CFAA8FE880A228AA578EC20BC6AF8F2* ___keypair_9;
	Il2CppSafeArray/*NONE*/* ___publicKey_10;
	Il2CppSafeArray/*NONE*/* ___keyToken_11;
	int32_t ___versioncompat_12;
	Version_tE426DB5655D0F22920AE16A2AA9AB7781B8255A7* ___version_13;
	int32_t ___processor_architecture_14;
	int32_t ___contentType_15;
};
// Native definition for COM marshalling of System.Reflection.AssemblyName
struct AssemblyName_t555F1570F523D87D970C6E7F27B1B44C83EADDD2_marshaled_com
{
	Il2CppChar* ___name_0;
	Il2CppChar* ___codebase_1;
	int32_t ___major_2;
	int32_t ___minor_3;
	int32_t ___build_4;
	int32_t ___revision_5;
	CultureInfo_t9BA817D41AD55AC8BD07480DD8AC22F8FFA378E0_marshaled_com* ___cultureinfo_6;
	int32_t ___flags_7;
	int32_t ___hashalg_8;
	StrongNameKeyPair_t0657447B6CFAA8FE880A228AA578EC20BC6AF8F2* ___keypair_9;
	Il2CppSafeArray/*NONE*/* ___publicKey_10;
	Il2CppSafeArray/*NONE*/* ___keyToken_11;
	int32_t ___versioncompat_12;
	Version_tE426DB5655D0F22920AE16A2AA9AB7781B8255A7* ___version_13;
	int32_t ___processor_architecture_14;
	int32_t ___contentType_15;
};

// System.Reflection.MemberInfo
struct MemberInfo_t  : public RuntimeObject
{
};

// System.String
struct String_t  : public RuntimeObject
{
	// System.Int32 System.String::_stringLength
	int32_t ____stringLength_4;
	// System.Char System.String::_firstChar
	Il2CppChar ____firstChar_5;
};

// System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
// Native definition for P/Invoke marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};

// System.Version
struct Version_tE426DB5655D0F22920AE16A2AA9AB7781B8255A7  : public RuntimeObject
{
	// System.Int32 System.Version::_Major
	int32_t ____Major_0;
	// System.Int32 System.Version::_Minor
	int32_t ____Minor_1;
	// System.Int32 System.Version::_Build
	int32_t ____Build_2;
	// System.Int32 System.Version::_Revision
	int32_t ____Revision_3;
};

// TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback/<>c__DisplayClass1_0
struct U3CU3Ec__DisplayClass1_0_tAEF0BA6A2A4D4C4291257AAECFC08872DE437A39  : public RuntimeObject
{
	// System.Boolean TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback/<>c__DisplayClass1_0::isSuccess
	bool ___isSuccess_0;
	// System.String TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback/<>c__DisplayClass1_0::message
	String_t* ___message_1;
	// System.String TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback/<>c__DisplayClass1_0::token
	String_t* ___token_2;
};

// TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback/<>c__DisplayClass2_0
struct U3CU3Ec__DisplayClass2_0_t34096128F462743F4ECF24F641BC27432EC60C01  : public RuntimeObject
{
	// System.Boolean TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback/<>c__DisplayClass2_0::isSuccess
	bool ___isSuccess_0;
	// System.String TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback/<>c__DisplayClass2_0::message
	String_t* ___message_1;
};

// UnityEngine.AndroidJavaClass
struct AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03  : public AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0
{
};

// System.Boolean
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	// System.Boolean System.Boolean::m_value
	bool ___m_value_0;
};

// System.Int32
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	// System.Int32 System.Int32::m_value
	int32_t ___m_value_0;
};

// System.IntPtr
struct IntPtr_t 
{
	// System.Void* System.IntPtr::m_value
	void* ___m_value_0;
};

// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};

// UnityEngine.AndroidJavaProxy
struct AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D  : public RuntimeObject
{
	// UnityEngine.AndroidJavaClass UnityEngine.AndroidJavaProxy::javaInterface
	AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* ___javaInterface_0;
	// System.IntPtr UnityEngine.AndroidJavaProxy::proxyObject
	intptr_t ___proxyObject_1;
};

// System.Delegate
struct Delegate_t  : public RuntimeObject
{
	// System.IntPtr System.Delegate::method_ptr
	intptr_t ___method_ptr_0;
	// System.IntPtr System.Delegate::invoke_impl
	intptr_t ___invoke_impl_1;
	// System.Object System.Delegate::m_target
	RuntimeObject* ___m_target_2;
	// System.IntPtr System.Delegate::method
	intptr_t ___method_3;
	// System.IntPtr System.Delegate::delegate_trampoline
	intptr_t ___delegate_trampoline_4;
	// System.IntPtr System.Delegate::extra_arg
	intptr_t ___extra_arg_5;
	// System.IntPtr System.Delegate::method_code
	intptr_t ___method_code_6;
	// System.IntPtr System.Delegate::interp_method
	intptr_t ___interp_method_7;
	// System.IntPtr System.Delegate::interp_invoke_impl
	intptr_t ___interp_invoke_impl_8;
	// System.Reflection.MethodInfo System.Delegate::method_info
	MethodInfo_t* ___method_info_9;
	// System.Reflection.MethodInfo System.Delegate::original_method_info
	MethodInfo_t* ___original_method_info_10;
	// System.DelegateData System.Delegate::data
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	// System.Boolean System.Delegate::method_is_virtual
	bool ___method_is_virtual_12;
};
// Native definition for P/Invoke marshalling of System.Delegate
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};
// Native definition for COM marshalling of System.Delegate
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};

// System.Exception
struct Exception_t  : public RuntimeObject
{
	// System.String System.Exception::_className
	String_t* ____className_1;
	// System.String System.Exception::_message
	String_t* ____message_2;
	// System.Collections.IDictionary System.Exception::_data
	RuntimeObject* ____data_3;
	// System.Exception System.Exception::_innerException
	Exception_t* ____innerException_4;
	// System.String System.Exception::_helpURL
	String_t* ____helpURL_5;
	// System.Object System.Exception::_stackTrace
	RuntimeObject* ____stackTrace_6;
	// System.String System.Exception::_stackTraceString
	String_t* ____stackTraceString_7;
	// System.String System.Exception::_remoteStackTraceString
	String_t* ____remoteStackTraceString_8;
	// System.Int32 System.Exception::_remoteStackIndex
	int32_t ____remoteStackIndex_9;
	// System.Object System.Exception::_dynamicMethods
	RuntimeObject* ____dynamicMethods_10;
	// System.Int32 System.Exception::_HResult
	int32_t ____HResult_11;
	// System.String System.Exception::_source
	String_t* ____source_12;
	// System.Runtime.Serialization.SafeSerializationManager System.Exception::_safeSerializationManager
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	// System.Diagnostics.StackTrace[] System.Exception::captured_traces
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	// System.IntPtr[] System.Exception::native_trace_ips
	IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832* ___native_trace_ips_15;
	// System.Int32 System.Exception::caught_in_unmanaged
	int32_t ___caught_in_unmanaged_16;
};
// Native definition for P/Invoke marshalling of System.Exception
struct Exception_t_marshaled_pinvoke
{
	char* ____className_1;
	char* ____message_2;
	RuntimeObject* ____data_3;
	Exception_t_marshaled_pinvoke* ____innerException_4;
	char* ____helpURL_5;
	Il2CppIUnknown* ____stackTrace_6;
	char* ____stackTraceString_7;
	char* ____remoteStackTraceString_8;
	int32_t ____remoteStackIndex_9;
	Il2CppIUnknown* ____dynamicMethods_10;
	int32_t ____HResult_11;
	char* ____source_12;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	Il2CppSafeArray/*NONE*/* ___native_trace_ips_15;
	int32_t ___caught_in_unmanaged_16;
};
// Native definition for COM marshalling of System.Exception
struct Exception_t_marshaled_com
{
	Il2CppChar* ____className_1;
	Il2CppChar* ____message_2;
	RuntimeObject* ____data_3;
	Exception_t_marshaled_com* ____innerException_4;
	Il2CppChar* ____helpURL_5;
	Il2CppIUnknown* ____stackTrace_6;
	Il2CppChar* ____stackTraceString_7;
	Il2CppChar* ____remoteStackTraceString_8;
	int32_t ____remoteStackIndex_9;
	Il2CppIUnknown* ____dynamicMethods_10;
	int32_t ____HResult_11;
	Il2CppChar* ____source_12;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	Il2CppSafeArray/*NONE*/* ___native_trace_ips_15;
	int32_t ___caught_in_unmanaged_16;
};

// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C  : public RuntimeObject
{
	// System.IntPtr UnityEngine.Object::m_CachedPtr
	intptr_t ___m_CachedPtr_0;
};
// Native definition for P/Invoke marshalling of UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
	intptr_t ___m_CachedPtr_0;
};
// Native definition for COM marshalling of UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
	intptr_t ___m_CachedPtr_0;
};

// System.RuntimeTypeHandle
struct RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B 
{
	// System.IntPtr System.RuntimeTypeHandle::value
	intptr_t ___value_0;
};

// System.MulticastDelegate
struct MulticastDelegate_t  : public Delegate_t
{
	// System.Delegate[] System.MulticastDelegate::delegates
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates_13;
};
// Native definition for P/Invoke marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates_13;
};
// Native definition for COM marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates_13;
};

// UnityEngine.ScriptableObject
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
// Native definition for P/Invoke marshalling of UnityEngine.ScriptableObject
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A_marshaled_pinvoke : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
};
// Native definition for COM marshalling of UnityEngine.ScriptableObject
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A_marshaled_com : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
};

// System.Type
struct Type_t  : public MemberInfo_t
{
	// System.RuntimeTypeHandle System.Type::_impl
	RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ____impl_8;
};

// TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback
struct BackendOnUnityCallback_tD10793DE59BC64A9A265FD049418D7CB3CD04362  : public AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D
{
};

// UnityEngine.AndroidJavaRunnable
struct AndroidJavaRunnable_tF23B9BBDA8C99A48BCEEA6335A47DA3C0EF34A7F  : public MulticastDelegate_t
{
};

// System.AsyncCallback
struct AsyncCallback_t7FEF460CBDCFB9C5FA2EF776984778B9A4145F4C  : public MulticastDelegate_t
{
};

// TheBackend.ToolKit.GoogleLogin.Settings.Android.TheBackendGoogleSettingsForAndroid
struct TheBackendGoogleSettingsForAndroid_t6A3D1A2835CFF801BB7FC6B06490CD842A278F47  : public ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A
{
	// System.String TheBackend.ToolKit.GoogleLogin.Settings.Android.TheBackendGoogleSettingsForAndroid::webClientID
	String_t* ___webClientID_4;
	// System.String TheBackend.ToolKit.GoogleLogin.Settings.Android.TheBackendGoogleSettingsForAndroid::androidSDKVersion
	String_t* ___androidSDKVersion_5;
};

// TheBackend.ToolKit.GoogleLogin.Android/GoogleLoginCallback
struct GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9  : public MulticastDelegate_t
{
};

// TheBackend.ToolKit.GoogleLogin.Android/GoogleSignOutCallback
struct GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2  : public MulticastDelegate_t
{
};

// <Module>

// <Module>

// System.EmptyArray`1<System.Object>
struct EmptyArray_1_tDF0DD7256B115243AA6BD5558417387A734240EE_StaticFields
{
	// T[] System.EmptyArray`1::Value
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___Value_0;
};

// System.EmptyArray`1<System.Object>

// TheBackend.ToolKit.GoogleLogin.Android
struct Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields
{
	// UnityEngine.AndroidJavaObject TheBackend.ToolKit.GoogleLogin.Android::backendFederationPluginInstance
	AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* ___backendFederationPluginInstance_3;
	// UnityEngine.AndroidJavaObject TheBackend.ToolKit.GoogleLogin.Android::unityActivity
	AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* ___unityActivity_4;
	// System.Object TheBackend.ToolKit.GoogleLogin.Android::lockObject
	RuntimeObject* ___lockObject_5;
	// TheBackend.ToolKit.GoogleLogin.Android/GoogleLoginCallback TheBackend.ToolKit.GoogleLogin.Android::OnGoogleLogin
	GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* ___OnGoogleLogin_6;
	// TheBackend.ToolKit.GoogleLogin.Android/GoogleSignOutCallback TheBackend.ToolKit.GoogleLogin.Android::OnGoogleSignOut
	GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* ___OnGoogleSignOut_7;
	// System.Boolean TheBackend.ToolKit.GoogleLogin.Android::isMainTreadCallback
	bool ___isMainTreadCallback_8;
};

// TheBackend.ToolKit.GoogleLogin.Android

// UnityEngine.AndroidJavaObject
struct AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_StaticFields
{
	// System.Boolean UnityEngine.AndroidJavaObject::enableDebugPrints
	bool ___enableDebugPrints_0;
};

// UnityEngine.AndroidJavaObject

// System.Reflection.Assembly

// System.Reflection.Assembly

// System.Reflection.AssemblyName

// System.Reflection.AssemblyName

// System.String
struct String_t_StaticFields
{
	// System.String System.String::Empty
	String_t* ___Empty_6;
};

// System.String

// System.Version

// System.Version

// TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback/<>c__DisplayClass1_0

// TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback/<>c__DisplayClass1_0

// TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback/<>c__DisplayClass2_0

// TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback/<>c__DisplayClass2_0

// UnityEngine.AndroidJavaClass

// UnityEngine.AndroidJavaClass

// System.Boolean
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	// System.String System.Boolean::TrueString
	String_t* ___TrueString_5;
	// System.String System.Boolean::FalseString
	String_t* ___FalseString_6;
};

// System.Boolean

// System.Int32

// System.Int32

// System.IntPtr
struct IntPtr_t_StaticFields
{
	// System.IntPtr System.IntPtr::Zero
	intptr_t ___Zero_1;
};

// System.IntPtr

// System.Void

// System.Void

// UnityEngine.AndroidJavaProxy
struct AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_StaticFields
{
	// UnityEngine.GlobalJavaObjectRef UnityEngine.AndroidJavaProxy::s_JavaLangSystemClass
	GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* ___s_JavaLangSystemClass_2;
	// System.IntPtr UnityEngine.AndroidJavaProxy::s_HashCodeMethodID
	intptr_t ___s_HashCodeMethodID_3;
};

// UnityEngine.AndroidJavaProxy

// System.Delegate

// System.Delegate

// System.Exception
struct Exception_t_StaticFields
{
	// System.Object System.Exception::s_EDILock
	RuntimeObject* ___s_EDILock_0;
};

// System.Exception

// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_StaticFields
{
	// System.Int32 UnityEngine.Object::OffsetOfInstanceIDInCPlusPlusObject
	int32_t ___OffsetOfInstanceIDInCPlusPlusObject_1;
};

// UnityEngine.Object

// System.RuntimeTypeHandle

// System.RuntimeTypeHandle

// System.Type
struct Type_t_StaticFields
{
	// System.Reflection.Binder modreq(System.Runtime.CompilerServices.IsVolatile) System.Type::s_defaultBinder
	Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235* ___s_defaultBinder_0;
	// System.Char System.Type::Delimiter
	Il2CppChar ___Delimiter_1;
	// System.Type[] System.Type::EmptyTypes
	TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB* ___EmptyTypes_2;
	// System.Object System.Type::Missing
	RuntimeObject* ___Missing_3;
	// System.Reflection.MemberFilter System.Type::FilterAttribute
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterAttribute_4;
	// System.Reflection.MemberFilter System.Type::FilterName
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterName_5;
	// System.Reflection.MemberFilter System.Type::FilterNameIgnoreCase
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterNameIgnoreCase_6;
};

// System.Type

// TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback

// TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback

// UnityEngine.AndroidJavaRunnable

// UnityEngine.AndroidJavaRunnable

// System.AsyncCallback

// System.AsyncCallback

// TheBackend.ToolKit.GoogleLogin.Settings.Android.TheBackendGoogleSettingsForAndroid

// TheBackend.ToolKit.GoogleLogin.Settings.Android.TheBackendGoogleSettingsForAndroid

// TheBackend.ToolKit.GoogleLogin.Android/GoogleLoginCallback

// TheBackend.ToolKit.GoogleLogin.Android/GoogleLoginCallback

// TheBackend.ToolKit.GoogleLogin.Android/GoogleSignOutCallback

// TheBackend.ToolKit.GoogleLogin.Android/GoogleSignOutCallback
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// System.Object[]
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918  : public RuntimeArray
{
	ALIGN_FIELD (8) RuntimeObject* m_Items[1];

	inline RuntimeObject* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, RuntimeObject* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline RuntimeObject* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, RuntimeObject* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
// System.Delegate[]
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771  : public RuntimeArray
{
	ALIGN_FIELD (8) Delegate_t* m_Items[1];

	inline Delegate_t* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Delegate_t** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Delegate_t* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline Delegate_t* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Delegate_t** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Delegate_t* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};


// FieldType UnityEngine.AndroidJavaObject::GetStatic<System.Object>(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* AndroidJavaObject_GetStatic_TisRuntimeObject_m4EF4E4761A0A6E99E0A298F653E8129B1494E4C9_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method) ;
// System.Void UnityEngine.AndroidJavaObject::SetStatic<System.Object>(System.String,FieldType)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_SetStatic_TisRuntimeObject_mB8836B4AC2D6DE9BDA3DFDA81D68B483301D3A37_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, RuntimeObject* ___1_val, const RuntimeMethod* method) ;
// T[] System.Array::Empty<System.Object>()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_gshared_inline (const RuntimeMethod* method) ;
// ReturnType UnityEngine.AndroidJavaObject::CallStatic<System.Object>(System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* AndroidJavaObject_CallStatic_TisRuntimeObject_mCAFE27630F6092C4910E14592B050DACFCBE146F_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;

// System.Type System.Type::GetTypeFromHandle(System.RuntimeTypeHandle)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Type_t* Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57 (RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ___0_handle, const RuntimeMethod* method) ;
// System.Version System.Reflection.AssemblyName::get_Version()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Version_tE426DB5655D0F22920AE16A2AA9AB7781B8255A7* AssemblyName_get_Version_mC20EC1E68FA7C40120112C2E29A19C9D948B5300_inline (AssemblyName_t555F1570F523D87D970C6E7F27B1B44C83EADDD2* __this, const RuntimeMethod* method) ;
// System.String System.Version::ToString(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Version_ToString_mC42C3A6D6F68C88C30DD6FA1B64A2EC99B2CB840 (Version_tE426DB5655D0F22920AE16A2AA9AB7781B8255A7* __this, int32_t ___0_fieldCount, const RuntimeMethod* method) ;
// System.Void System.Threading.Monitor::Exit(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Monitor_Exit_m05B2CF037E2214B3208198C282490A2A475653FA (RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
// System.Void System.Threading.Monitor::Enter(System.Object,System.Boolean&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Monitor_Enter_m3CDB589DA1300B513D55FDCFB52B63E879794149 (RuntimeObject* ___0_obj, bool* ___1_lockTaken, const RuntimeMethod* method) ;
// System.Void UnityEngine.AndroidJavaClass::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaClass__ctor_mB5466169E1151B8CC44C8FED234D79984B431389 (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* __this, String_t* ___0_className, const RuntimeMethod* method) ;
// FieldType UnityEngine.AndroidJavaObject::GetStatic<UnityEngine.AndroidJavaObject>(System.String)
inline AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* AndroidJavaObject_GetStatic_TisAndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_mD7D192A35EB2B2DA3775FAB081958B72088251DD (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method)
{
	return ((  AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* (*) (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*, String_t*, const RuntimeMethod*))AndroidJavaObject_GetStatic_TisRuntimeObject_m4EF4E4761A0A6E99E0A298F653E8129B1494E4C9_gshared)(__this, ___0_fieldName, method);
}
// System.Void UnityEngine.AndroidJavaObject::SetStatic<UnityEngine.AndroidJavaObject>(System.String,FieldType)
inline void AndroidJavaObject_SetStatic_TisAndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_m52D5B3006350B7FB0EAE6B2E2A37EDD1606F01D1 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* ___1_val, const RuntimeMethod* method)
{
	((  void (*) (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*, String_t*, AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*, const RuntimeMethod*))AndroidJavaObject_SetStatic_TisRuntimeObject_mB8836B4AC2D6DE9BDA3DFDA81D68B483301D3A37_gshared)(__this, ___0_fieldName, ___1_val, method);
}
// T[] System.Array::Empty<System.Object>()
inline ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_inline (const RuntimeMethod* method)
{
	return ((  ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* (*) (const RuntimeMethod*))Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_gshared_inline)(method);
}
// ReturnType UnityEngine.AndroidJavaObject::CallStatic<UnityEngine.AndroidJavaObject>(System.String,System.Object[])
inline AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* AndroidJavaObject_CallStatic_TisAndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_m398EA96C1DE1BB885F2B1DD0E00E8BBA86B49E63 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method)
{
	return ((  AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* (*) (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*, String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, const RuntimeMethod*))AndroidJavaObject_CallStatic_TisRuntimeObject_mCAFE27630F6092C4910E14592B050DACFCBE146F_gshared)(__this, ___0_methodName, ___1_args, method);
}
// UnityEngine.Object UnityEngine.Resources::Load(System.String,System.Type)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* Resources_Load_m6CD8FBBCCFFF22179FA0E7B1806B888103008D33 (String_t* ___0_path, Type_t* ___1_systemTypeInstance, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Object::op_Equality(UnityEngine.Object,UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_x, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___1_y, const RuntimeMethod* method) ;
// System.Void System.Exception::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F (Exception_t* __this, String_t* ___0_message, const RuntimeMethod* method) ;
// System.Boolean System.String::IsNullOrEmpty(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_IsNullOrEmpty_mEA9E3FB005AC28FE02E69FCF95A7B8456192B478 (String_t* ___0_value, const RuntimeMethod* method) ;
// System.Void TheBackend.ToolKit.GoogleLogin.Android::GoogleLogin(System.String,System.Boolean,TheBackend.ToolKit.GoogleLogin.Android/GoogleLoginCallback)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Android_GoogleLogin_mFCCA40B96FD043C48036C73245D57BF73038E903 (String_t* ___0_webClientId, bool ___1_isCallbackInMainThread, GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* ___2_googleLoginCallback, const RuntimeMethod* method) ;
// System.String System.String::Concat(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m9E3155FB84015C823606188F53B47CB44C444991 (String_t* ___0_str0, String_t* ___1_str1, const RuntimeMethod* method) ;
// System.Void TheBackend.ToolKit.GoogleLogin.Android/GoogleLoginCallback::Invoke(System.Boolean,System.String,System.String)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void GoogleLoginCallback_Invoke_mC58ED5473ED0705F936988D1EE2E874181E92411_inline (GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* __this, bool ___0_isSuccess, String_t* ___1_message, String_t* ___2_token, const RuntimeMethod* method) ;
// UnityEngine.RuntimePlatform UnityEngine.Application::get_platform()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Application_get_platform_m59EF7D6155D18891B24767F83F388160B1FF2138 (const RuntimeMethod* method) ;
// System.String System.String::Format(System.String,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_mA8DBB4C2516B9723C5A41E6CB1E2FAF4BBE96DD8 (String_t* ___0_format, RuntimeObject* ___1_arg0, const RuntimeMethod* method) ;
// UnityEngine.AndroidJavaObject TheBackend.ToolKit.GoogleLogin.Android::get_PluginInstance()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* Android_get_PluginInstance_m917082A5EBA89BB894886E94C1D159080FDFBE17 (const RuntimeMethod* method) ;
// System.Void TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BackendOnUnityCallback__ctor_mCD40E5E8D5C513520B85E092426B369308D61E6A (BackendOnUnityCallback_tD10793DE59BC64A9A265FD049418D7CB3CD04362* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.AndroidJavaObject::Call(System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_Call_mDEF7846E2AB1C5379069BB21049ED55A9D837B1C (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
// System.Void TheBackend.ToolKit.GoogleLogin.Android/GoogleSignOutCallback::Invoke(System.Boolean,System.String)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void GoogleSignOutCallback_Invoke_mCB1EB05A50940E5E9BB2FF2CEC2A918C1EF2F281_inline (GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* __this, bool ___0_isSuccess, String_t* ___1_message, const RuntimeMethod* method) ;
// System.Void System.Object::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.AndroidJavaProxy::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaProxy__ctor_m2832886A0E1BBF6702653A7C6A4609F11FB712C7 (AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D* __this, String_t* ___0_javaInterface, const RuntimeMethod* method) ;
// System.Void TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback/<>c__DisplayClass1_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass1_0__ctor_m20D9D45C0B43079063E3764D7124661EEBB62D47 (U3CU3Ec__DisplayClass1_0_tAEF0BA6A2A4D4C4291257AAECFC08872DE437A39* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.AndroidJavaRunnable::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaRunnable__ctor_m000E4FEB2DE8031A1CD733610D76E2BF60490334 (AndroidJavaRunnable_tF23B9BBDA8C99A48BCEEA6335A47DA3C0EF34A7F* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
// System.Void TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback/<>c__DisplayClass2_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass2_0__ctor_mF838BA657A2E50DD03361E71F4856EAD8EFE8A41 (U3CU3Ec__DisplayClass2_0_t34096128F462743F4ECF24F641BC27432EC60C01* __this, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.String TheBackend.ToolKit.GoogleLogin.Android::__Version()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Android___Version_m2421608BDF54A87BBA4E49453DBD7CC2EC26B753 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_0_0_0_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Type_t_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_0_0_0_var) };
		il2cpp_codegen_runtime_class_init_inline(Type_t_il2cpp_TypeInfo_var);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		Assembly_t* L_2;
		L_2 = VirtualFuncInvoker0< Assembly_t* >::Invoke(27 /* System.Reflection.Assembly System.Type::get_Assembly() */, L_1);
		NullCheck(L_2);
		AssemblyName_t555F1570F523D87D970C6E7F27B1B44C83EADDD2* L_3;
		L_3 = VirtualFuncInvoker0< AssemblyName_t555F1570F523D87D970C6E7F27B1B44C83EADDD2* >::Invoke(22 /* System.Reflection.AssemblyName System.Reflection.Assembly::GetName() */, L_2);
		NullCheck(L_3);
		Version_tE426DB5655D0F22920AE16A2AA9AB7781B8255A7* L_4;
		L_4 = AssemblyName_get_Version_mC20EC1E68FA7C40120112C2E29A19C9D948B5300_inline(L_3, NULL);
		NullCheck(L_4);
		String_t* L_5;
		L_5 = Version_ToString_mC42C3A6D6F68C88C30DD6FA1B64A2EC99B2CB840(L_4, 3, NULL);
		return L_5;
	}
}
// UnityEngine.AndroidJavaObject TheBackend.ToolKit.GoogleLogin.Android::get_PluginInstance()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* Android_get_PluginInstance_m917082A5EBA89BB894886E94C1D159080FDFBE17 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_CallStatic_TisAndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_m398EA96C1DE1BB885F2B1DD0E00E8BBA86B49E63_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_GetStatic_TisAndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_mD7D192A35EB2B2DA3775FAB081958B72088251DD_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_SetStatic_TisAndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_m52D5B3006350B7FB0EAE6B2E2A37EDD1606F01D1_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2063737B07B6658BC2E1EC3128D4E09E57CA123E);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral4D31205BF9E55201691DD5151AE8015105AF3477);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral4D613657609485AE586A3379BA0E3FC13C1E1078);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5C85C813951C1B718C305DF0F28FF52E4C670727);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralFB4AE4F77150C3A8E8E4F8B23E734E0C7277B7D9);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	bool V_1 = false;
	{
		il2cpp_codegen_runtime_class_init_inline(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_0 = ((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___backendFederationPluginInstance_3;
		if (L_0)
		{
			goto IL_0071;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
		RuntimeObject* L_1 = ((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___lockObject_5;
		V_0 = L_1;
		V_1 = (bool)0;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0067:
			{// begin finally (depth: 1)
				{
					bool L_2 = V_1;
					if (!L_2)
					{
						goto IL_0070;
					}
				}
				{
					RuntimeObject* L_3 = V_0;
					Monitor_Exit_m05B2CF037E2214B3208198C282490A2A475653FA(L_3, NULL);
				}

IL_0070:
				{
					return;
				}
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				RuntimeObject* L_4 = V_0;
				Monitor_Enter_m3CDB589DA1300B513D55FDCFB52B63E879794149(L_4, (&V_1), NULL);
				il2cpp_codegen_runtime_class_init_inline(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
				AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_5 = ((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___backendFederationPluginInstance_3;
				if (L_5)
				{
					goto IL_0065_1;
				}
			}
			{
				AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_6 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)il2cpp_codegen_object_new(AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
				NullCheck(L_6);
				AndroidJavaClass__ctor_mB5466169E1151B8CC44C8FED234D79984B431389(L_6, _stringLiteral5C85C813951C1B718C305DF0F28FF52E4C670727, NULL);
				AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_7 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)il2cpp_codegen_object_new(AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
				NullCheck(L_7);
				AndroidJavaClass__ctor_mB5466169E1151B8CC44C8FED234D79984B431389(L_7, _stringLiteral4D613657609485AE586A3379BA0E3FC13C1E1078, NULL);
				NullCheck(L_7);
				AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_8;
				L_8 = AndroidJavaObject_GetStatic_TisAndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_mD7D192A35EB2B2DA3775FAB081958B72088251DD(L_7, _stringLiteralFB4AE4F77150C3A8E8E4F8B23E734E0C7277B7D9, AndroidJavaObject_GetStatic_TisAndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_mD7D192A35EB2B2DA3775FAB081958B72088251DD_RuntimeMethod_var);
				il2cpp_codegen_runtime_class_init_inline(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
				((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___unityActivity_4 = L_8;
				Il2CppCodeGenWriteBarrier((void**)(&((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___unityActivity_4), (void*)L_8);
				AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_9 = L_6;
				AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_10 = ((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___unityActivity_4;
				NullCheck(L_9);
				AndroidJavaObject_SetStatic_TisAndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_m52D5B3006350B7FB0EAE6B2E2A37EDD1606F01D1(L_9, _stringLiteral4D31205BF9E55201691DD5151AE8015105AF3477, L_10, AndroidJavaObject_SetStatic_TisAndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_m52D5B3006350B7FB0EAE6B2E2A37EDD1606F01D1_RuntimeMethod_var);
				ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_11;
				L_11 = Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_inline(Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_RuntimeMethod_var);
				NullCheck(L_9);
				AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_12;
				L_12 = AndroidJavaObject_CallStatic_TisAndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_m398EA96C1DE1BB885F2B1DD0E00E8BBA86B49E63(L_9, _stringLiteral2063737B07B6658BC2E1EC3128D4E09E57CA123E, L_11, AndroidJavaObject_CallStatic_TisAndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_m398EA96C1DE1BB885F2B1DD0E00E8BBA86B49E63_RuntimeMethod_var);
				((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___backendFederationPluginInstance_3 = L_12;
				Il2CppCodeGenWriteBarrier((void**)(&((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___backendFederationPluginInstance_3), (void*)L_12);
			}

IL_0065_1:
			{
				goto IL_0071;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0071:
	{
		il2cpp_codegen_runtime_class_init_inline(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_13 = ((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___backendFederationPluginInstance_3;
		return L_13;
	}
}
// System.Void TheBackend.ToolKit.GoogleLogin.Android::GoogleLogin(System.Boolean,TheBackend.ToolKit.GoogleLogin.Android/GoogleLoginCallback)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Android_GoogleLogin_mCBCB7FE461475F404B56E68B1563A50FF843116E (bool ___0_isCallbackInMainThread, GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* ___1_googleLoginCallback, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TheBackendGoogleSettingsForAndroid_t6A3D1A2835CFF801BB7FC6B06490CD842A278F47_0_0_0_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TheBackendGoogleSettingsForAndroid_t6A3D1A2835CFF801BB7FC6B06490CD842A278F47_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Type_t_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralB4D25E0E5E2D132E156F790B434A50D5CB83E8EE);
		s_Il2CppMethodInitialized = true;
	}
	Exception_t* V_0 = NULL;
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	TheBackendGoogleSettingsForAndroid_t6A3D1A2835CFF801BB7FC6B06490CD842A278F47* G_B2_0 = NULL;
	TheBackendGoogleSettingsForAndroid_t6A3D1A2835CFF801BB7FC6B06490CD842A278F47* G_B1_0 = NULL;
	TheBackendGoogleSettingsForAndroid_t6A3D1A2835CFF801BB7FC6B06490CD842A278F47* G_B4_0 = NULL;
	TheBackendGoogleSettingsForAndroid_t6A3D1A2835CFF801BB7FC6B06490CD842A278F47* G_B3_0 = NULL;
	try
	{// begin try (depth: 1)
		{
			RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (TheBackendGoogleSettingsForAndroid_t6A3D1A2835CFF801BB7FC6B06490CD842A278F47_0_0_0_var) };
			il2cpp_codegen_runtime_class_init_inline(Type_t_il2cpp_TypeInfo_var);
			Type_t* L_1;
			L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
			Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_2;
			L_2 = Resources_Load_m6CD8FBBCCFFF22179FA0E7B1806B888103008D33(_stringLiteralB4D25E0E5E2D132E156F790B434A50D5CB83E8EE, L_1, NULL);
			TheBackendGoogleSettingsForAndroid_t6A3D1A2835CFF801BB7FC6B06490CD842A278F47* L_3 = ((TheBackendGoogleSettingsForAndroid_t6A3D1A2835CFF801BB7FC6B06490CD842A278F47*)CastclassClass((RuntimeObject*)L_2, TheBackendGoogleSettingsForAndroid_t6A3D1A2835CFF801BB7FC6B06490CD842A278F47_il2cpp_TypeInfo_var));
			il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
			bool L_4;
			L_4 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605(L_3, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
			G_B1_0 = L_3;
			if (!L_4)
			{
				G_B2_0 = L_3;
				goto IL_002d_1;
			}
		}
		{
			Exception_t* L_5 = (Exception_t*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var)));
			NullCheck(L_5);
			Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_5, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42CD92DB2E10FD2A1DDC98FE055DC8D4CCCFDB06)), NULL);
			IL2CPP_RAISE_MANAGED_EXCEPTION(L_5, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Android_GoogleLogin_mCBCB7FE461475F404B56E68B1563A50FF843116E_RuntimeMethod_var)));
		}

IL_002d_1:
		{
			TheBackendGoogleSettingsForAndroid_t6A3D1A2835CFF801BB7FC6B06490CD842A278F47* L_6 = G_B2_0;
			NullCheck(L_6);
			String_t* L_7 = L_6->___webClientID_4;
			bool L_8;
			L_8 = String_IsNullOrEmpty_mEA9E3FB005AC28FE02E69FCF95A7B8456192B478(L_7, NULL);
			G_B3_0 = L_6;
			if (!L_8)
			{
				G_B4_0 = L_6;
				goto IL_0045_1;
			}
		}
		{
			Exception_t* L_9 = (Exception_t*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var)));
			NullCheck(L_9);
			Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_9, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralDF61F088F4F72BC95F76E46C54EDD133EEF6C597)), NULL);
			IL2CPP_RAISE_MANAGED_EXCEPTION(L_9, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Android_GoogleLogin_mCBCB7FE461475F404B56E68B1563A50FF843116E_RuntimeMethod_var)));
		}

IL_0045_1:
		{
			NullCheck(G_B4_0);
			String_t* L_10 = G_B4_0->___webClientID_4;
			bool L_11 = ___0_isCallbackInMainThread;
			GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* L_12 = ___1_googleLoginCallback;
			il2cpp_codegen_runtime_class_init_inline(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
			Android_GoogleLogin_mFCCA40B96FD043C48036C73245D57BF73038E903(L_10, L_11, L_12, NULL);
			goto IL_0072;
		}
	}// end try (depth: 1)
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0053;
		}
		throw e;
	}

CATCH_0053:
	{// begin catch(System.Exception)
		Exception_t* L_13 = ((Exception_t*)IL2CPP_GET_ACTIVE_EXCEPTION(Exception_t*));;
		V_0 = L_13;
		GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* L_14 = ___1_googleLoginCallback;
		Exception_t* L_15 = V_0;
		NullCheck(L_15);
		String_t* L_16;
		L_16 = VirtualFuncInvoker0< String_t* >::Invoke(5 /* System.String System.Exception::get_Message() */, L_15);
		String_t* L_17;
		L_17 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralAE97D0ED1749ECB596B76299EE34E3F2291ED681)), L_16, NULL);
		String_t* L_18 = ((String_t_StaticFields*)il2cpp_codegen_static_fields_for(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&String_t_il2cpp_TypeInfo_var))))->___Empty_6;
		NullCheck(L_14);
		GoogleLoginCallback_Invoke_mC58ED5473ED0705F936988D1EE2E874181E92411_inline(L_14, (bool)0, L_17, L_18, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_0072;
	}// end catch (depth: 1)

IL_0072:
	{
		return;
	}
}
// System.Void TheBackend.ToolKit.GoogleLogin.Android::GoogleLogin(System.String,System.Boolean,TheBackend.ToolKit.GoogleLogin.Android/GoogleLoginCallback)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Android_GoogleLogin_mFCCA40B96FD043C48036C73245D57BF73038E903 (String_t* ___0_webClientId, bool ___1_isCallbackInMainThread, GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* ___2_googleLoginCallback, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BackendOnUnityCallback_tD10793DE59BC64A9A265FD049418D7CB3CD04362_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral03272EFE2FAACA7C9ECAA4C85414177AF5115A02);
		s_Il2CppMethodInitialized = true;
	}
	Exception_t* V_0 = NULL;
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	try
	{// begin try (depth: 1)
		{
			bool L_0 = ___1_isCallbackInMainThread;
			il2cpp_codegen_runtime_class_init_inline(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
			((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___isMainTreadCallback_8 = L_0;
			int32_t L_1;
			L_1 = Application_get_platform_m59EF7D6155D18891B24767F83F388160B1FF2138(NULL);
			if ((((int32_t)L_1) == ((int32_t)((int32_t)11))))
			{
				goto IL_0029_1;
			}
		}
		{
			int32_t L_2;
			L_2 = Application_get_platform_m59EF7D6155D18891B24767F83F388160B1FF2138(NULL);
			int32_t L_3 = L_2;
			RuntimeObject* L_4 = Box(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&RuntimePlatform_t9A8AAF204603076FCAAECCCC05DA386AEE7BF66E_il2cpp_TypeInfo_var)), &L_3);
			String_t* L_5;
			L_5 = String_Format_mA8DBB4C2516B9723C5A41E6CB1E2FAF4BBE96DD8(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral5085336E5FDBAF02EB48DC1A34A9A1636E1677FB)), L_4, NULL);
			Exception_t* L_6 = (Exception_t*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var)));
			NullCheck(L_6);
			Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_6, L_5, NULL);
			IL2CPP_RAISE_MANAGED_EXCEPTION(L_6, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Android_GoogleLogin_mFCCA40B96FD043C48036C73245D57BF73038E903_RuntimeMethod_var)));
		}

IL_0029_1:
		{
			GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* L_7 = ___2_googleLoginCallback;
			il2cpp_codegen_runtime_class_init_inline(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
			((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___OnGoogleLogin_6 = L_7;
			Il2CppCodeGenWriteBarrier((void**)(&((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___OnGoogleLogin_6), (void*)L_7);
			AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_8;
			L_8 = Android_get_PluginInstance_m917082A5EBA89BB894886E94C1D159080FDFBE17(NULL);
			ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_9 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)2);
			ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_10 = L_9;
			String_t* L_11 = ___0_webClientId;
			NullCheck(L_10);
			ArrayElementTypeCheck (L_10, L_11);
			(L_10)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_11);
			ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_12 = L_10;
			BackendOnUnityCallback_tD10793DE59BC64A9A265FD049418D7CB3CD04362* L_13 = (BackendOnUnityCallback_tD10793DE59BC64A9A265FD049418D7CB3CD04362*)il2cpp_codegen_object_new(BackendOnUnityCallback_tD10793DE59BC64A9A265FD049418D7CB3CD04362_il2cpp_TypeInfo_var);
			NullCheck(L_13);
			BackendOnUnityCallback__ctor_mCD40E5E8D5C513520B85E092426B369308D61E6A(L_13, NULL);
			NullCheck(L_12);
			ArrayElementTypeCheck (L_12, L_13);
			(L_12)->SetAt(static_cast<il2cpp_array_size_t>(1), (RuntimeObject*)L_13);
			NullCheck(L_8);
			AndroidJavaObject_Call_mDEF7846E2AB1C5379069BB21049ED55A9D837B1C(L_8, _stringLiteral03272EFE2FAACA7C9ECAA4C85414177AF5115A02, L_12, NULL);
			goto IL_0071;
		}
	}// end try (depth: 1)
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0052;
		}
		throw e;
	}

CATCH_0052:
	{// begin catch(System.Exception)
		Exception_t* L_14 = ((Exception_t*)IL2CPP_GET_ACTIVE_EXCEPTION(Exception_t*));;
		V_0 = L_14;
		GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* L_15 = ___2_googleLoginCallback;
		Exception_t* L_16 = V_0;
		NullCheck(L_16);
		String_t* L_17;
		L_17 = VirtualFuncInvoker0< String_t* >::Invoke(5 /* System.String System.Exception::get_Message() */, L_16);
		String_t* L_18;
		L_18 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralAE97D0ED1749ECB596B76299EE34E3F2291ED681)), L_17, NULL);
		String_t* L_19 = ((String_t_StaticFields*)il2cpp_codegen_static_fields_for(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&String_t_il2cpp_TypeInfo_var))))->___Empty_6;
		NullCheck(L_15);
		GoogleLoginCallback_Invoke_mC58ED5473ED0705F936988D1EE2E874181E92411_inline(L_15, (bool)0, L_18, L_19, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_0071;
	}// end catch (depth: 1)

IL_0071:
	{
		return;
	}
}
// System.Void TheBackend.ToolKit.GoogleLogin.Android::GoogleSignOut(System.Boolean,TheBackend.ToolKit.GoogleLogin.Android/GoogleSignOutCallback)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Android_GoogleSignOut_m3CF767DFF1D93908801CDC2FF53449F6DEC520CD (bool ___0_isCallbackInMainThread, GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* ___1_googleSignOutCallback, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BackendOnUnityCallback_tD10793DE59BC64A9A265FD049418D7CB3CD04362_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral3BC29E947F18FE016F459842215F6335AB7D9610);
		s_Il2CppMethodInitialized = true;
	}
	Exception_t* V_0 = NULL;
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	try
	{// begin try (depth: 1)
		{
			bool L_0 = ___0_isCallbackInMainThread;
			il2cpp_codegen_runtime_class_init_inline(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
			((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___isMainTreadCallback_8 = L_0;
			int32_t L_1;
			L_1 = Application_get_platform_m59EF7D6155D18891B24767F83F388160B1FF2138(NULL);
			if ((((int32_t)L_1) == ((int32_t)((int32_t)11))))
			{
				goto IL_0029_1;
			}
		}
		{
			int32_t L_2;
			L_2 = Application_get_platform_m59EF7D6155D18891B24767F83F388160B1FF2138(NULL);
			int32_t L_3 = L_2;
			RuntimeObject* L_4 = Box(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&RuntimePlatform_t9A8AAF204603076FCAAECCCC05DA386AEE7BF66E_il2cpp_TypeInfo_var)), &L_3);
			String_t* L_5;
			L_5 = String_Format_mA8DBB4C2516B9723C5A41E6CB1E2FAF4BBE96DD8(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral5085336E5FDBAF02EB48DC1A34A9A1636E1677FB)), L_4, NULL);
			Exception_t* L_6 = (Exception_t*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var)));
			NullCheck(L_6);
			Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_6, L_5, NULL);
			IL2CPP_RAISE_MANAGED_EXCEPTION(L_6, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Android_GoogleSignOut_m3CF767DFF1D93908801CDC2FF53449F6DEC520CD_RuntimeMethod_var)));
		}

IL_0029_1:
		{
			GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* L_7 = ___1_googleSignOutCallback;
			il2cpp_codegen_runtime_class_init_inline(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
			((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___OnGoogleSignOut_7 = L_7;
			Il2CppCodeGenWriteBarrier((void**)(&((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___OnGoogleSignOut_7), (void*)L_7);
			AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_8;
			L_8 = Android_get_PluginInstance_m917082A5EBA89BB894886E94C1D159080FDFBE17(NULL);
			ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_9 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)1);
			ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_10 = L_9;
			BackendOnUnityCallback_tD10793DE59BC64A9A265FD049418D7CB3CD04362* L_11 = (BackendOnUnityCallback_tD10793DE59BC64A9A265FD049418D7CB3CD04362*)il2cpp_codegen_object_new(BackendOnUnityCallback_tD10793DE59BC64A9A265FD049418D7CB3CD04362_il2cpp_TypeInfo_var);
			NullCheck(L_11);
			BackendOnUnityCallback__ctor_mCD40E5E8D5C513520B85E092426B369308D61E6A(L_11, NULL);
			NullCheck(L_10);
			ArrayElementTypeCheck (L_10, L_11);
			(L_10)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_11);
			NullCheck(L_8);
			AndroidJavaObject_Call_mDEF7846E2AB1C5379069BB21049ED55A9D837B1C(L_8, _stringLiteral3BC29E947F18FE016F459842215F6335AB7D9610, L_10, NULL);
			goto IL_0068;
		}
	}// end try (depth: 1)
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_004e;
		}
		throw e;
	}

CATCH_004e:
	{// begin catch(System.Exception)
		Exception_t* L_12 = ((Exception_t*)IL2CPP_GET_ACTIVE_EXCEPTION(Exception_t*));;
		V_0 = L_12;
		GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* L_13 = ___1_googleSignOutCallback;
		Exception_t* L_14 = V_0;
		NullCheck(L_14);
		String_t* L_15;
		L_15 = VirtualFuncInvoker0< String_t* >::Invoke(5 /* System.String System.Exception::get_Message() */, L_14);
		String_t* L_16;
		L_16 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralAE97D0ED1749ECB596B76299EE34E3F2291ED681)), L_15, NULL);
		NullCheck(L_13);
		GoogleSignOutCallback_Invoke_mCB1EB05A50940E5E9BB2FF2CEC2A918C1EF2F281_inline(L_13, (bool)0, L_16, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_0068;
	}// end catch (depth: 1)

IL_0068:
	{
		return;
	}
}
// System.Void TheBackend.ToolKit.GoogleLogin.Android::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Android__ctor_mD3472B9B3BE643FA9937048E0AC1D42041CCA503 (Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Void TheBackend.ToolKit.GoogleLogin.Android::.cctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Android__cctor_m693573504F8924C38AC69A27D5B41F7D906B08E3 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RuntimeObject_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		RuntimeObject* L_0 = (RuntimeObject*)il2cpp_codegen_object_new(RuntimeObject_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(L_0, NULL);
		((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___lockObject_5 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___lockObject_5), (void*)L_0);
		((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___isMainTreadCallback_8 = (bool)1;
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
void GoogleLoginCallback_Invoke_mC58ED5473ED0705F936988D1EE2E874181E92411_Multicast(GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* __this, bool ___0_isSuccess, String_t* ___1_message, String_t* ___2_token, const RuntimeMethod* method)
{
	il2cpp_array_size_t length = __this->___delegates_13->max_length;
	Delegate_t** delegatesToInvoke = reinterpret_cast<Delegate_t**>(__this->___delegates_13->GetAddressAtUnchecked(0));
	for (il2cpp_array_size_t i = 0; i < length; i++)
	{
		GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* currentDelegate = reinterpret_cast<GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9*>(delegatesToInvoke[i]);
		typedef void (*FunctionPointerType) (RuntimeObject*, bool, String_t*, String_t*, const RuntimeMethod*);
		((FunctionPointerType)currentDelegate->___invoke_impl_1)((Il2CppObject*)currentDelegate->___method_code_6, ___0_isSuccess, ___1_message, ___2_token, reinterpret_cast<RuntimeMethod*>(currentDelegate->___method_3));
	}
}
void GoogleLoginCallback_Invoke_mC58ED5473ED0705F936988D1EE2E874181E92411_OpenInst(GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* __this, bool ___0_isSuccess, String_t* ___1_message, String_t* ___2_token, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (bool, String_t*, String_t*, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr_0)(___0_isSuccess, ___1_message, ___2_token, method);
}
void GoogleLoginCallback_Invoke_mC58ED5473ED0705F936988D1EE2E874181E92411_OpenStatic(GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* __this, bool ___0_isSuccess, String_t* ___1_message, String_t* ___2_token, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (bool, String_t*, String_t*, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr_0)(___0_isSuccess, ___1_message, ___2_token, method);
}
void GoogleLoginCallback_Invoke_mC58ED5473ED0705F936988D1EE2E874181E92411_OpenStaticInvoker(GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* __this, bool ___0_isSuccess, String_t* ___1_message, String_t* ___2_token, const RuntimeMethod* method)
{
	InvokerActionInvoker3< bool, String_t*, String_t* >::Invoke((Il2CppMethodPointer)__this->___method_ptr_0, method, NULL, ___0_isSuccess, ___1_message, ___2_token);
}
void GoogleLoginCallback_Invoke_mC58ED5473ED0705F936988D1EE2E874181E92411_ClosedStaticInvoker(GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* __this, bool ___0_isSuccess, String_t* ___1_message, String_t* ___2_token, const RuntimeMethod* method)
{
	InvokerActionInvoker4< RuntimeObject*, bool, String_t*, String_t* >::Invoke((Il2CppMethodPointer)__this->___method_ptr_0, method, NULL, __this->___m_target_2, ___0_isSuccess, ___1_message, ___2_token);
}
IL2CPP_EXTERN_C  void DelegatePInvokeWrapper_GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9 (GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* __this, bool ___0_isSuccess, String_t* ___1_message, String_t* ___2_token, const RuntimeMethod* method)
{
	typedef void (DEFAULT_CALL *PInvokeFunc)(int32_t, char*, char*);
	PInvokeFunc il2cppPInvokeFunc = reinterpret_cast<PInvokeFunc>(il2cpp_codegen_get_reverse_pinvoke_function_ptr(__this));
	// Marshaling of parameter '___1_message' to native representation
	char* ____1_message_marshaled = NULL;
	____1_message_marshaled = il2cpp_codegen_marshal_string(___1_message);

	// Marshaling of parameter '___2_token' to native representation
	char* ____2_token_marshaled = NULL;
	____2_token_marshaled = il2cpp_codegen_marshal_string(___2_token);

	// Native function invocation
	il2cppPInvokeFunc(static_cast<int32_t>(___0_isSuccess), ____1_message_marshaled, ____2_token_marshaled);

	// Marshaling cleanup of parameter '___1_message' native representation
	il2cpp_codegen_marshal_free(____1_message_marshaled);
	____1_message_marshaled = NULL;

	// Marshaling cleanup of parameter '___2_token' native representation
	il2cpp_codegen_marshal_free(____2_token_marshaled);
	____2_token_marshaled = NULL;

}
// System.Void TheBackend.ToolKit.GoogleLogin.Android/GoogleLoginCallback::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GoogleLoginCallback__ctor_m3B110114AC199A6470D6C1983326042D641F321D (GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) 
{
	__this->___method_ptr_0 = (intptr_t)il2cpp_codegen_get_virtual_call_method_pointer((RuntimeMethod*)___1_method);
	__this->___method_3 = ___1_method;
	__this->___m_target_2 = ___0_object;
	Il2CppCodeGenWriteBarrier((void**)(&__this->___m_target_2), (void*)___0_object);
	int parameterCount = il2cpp_codegen_method_parameter_count((RuntimeMethod*)___1_method);
	__this->___method_code_6 = (intptr_t)__this;
	if (MethodIsStatic((RuntimeMethod*)___1_method))
	{
		bool isOpen = parameterCount == 3;
		if (il2cpp_codegen_call_method_via_invoker((RuntimeMethod*)___1_method))
			if (isOpen)
				__this->___invoke_impl_1 = (intptr_t)&GoogleLoginCallback_Invoke_mC58ED5473ED0705F936988D1EE2E874181E92411_OpenStaticInvoker;
			else
				__this->___invoke_impl_1 = (intptr_t)&GoogleLoginCallback_Invoke_mC58ED5473ED0705F936988D1EE2E874181E92411_ClosedStaticInvoker;
		else
			if (isOpen)
				__this->___invoke_impl_1 = (intptr_t)&GoogleLoginCallback_Invoke_mC58ED5473ED0705F936988D1EE2E874181E92411_OpenStatic;
			else
				{
					__this->___invoke_impl_1 = __this->___method_ptr_0;
					__this->___method_code_6 = (intptr_t)__this->___m_target_2;
				}
	}
	else
	{
		if (___0_object == NULL)
			il2cpp_codegen_raise_exception(il2cpp_codegen_get_argument_exception(NULL, "Delegate to an instance method cannot have null 'this'."), NULL);
		__this->___invoke_impl_1 = __this->___method_ptr_0;
		__this->___method_code_6 = (intptr_t)__this->___m_target_2;
	}
	__this->___extra_arg_5 = (intptr_t)&GoogleLoginCallback_Invoke_mC58ED5473ED0705F936988D1EE2E874181E92411_Multicast;
}
// System.Void TheBackend.ToolKit.GoogleLogin.Android/GoogleLoginCallback::Invoke(System.Boolean,System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GoogleLoginCallback_Invoke_mC58ED5473ED0705F936988D1EE2E874181E92411 (GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* __this, bool ___0_isSuccess, String_t* ___1_message, String_t* ___2_token, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, bool, String_t*, String_t*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___0_isSuccess, ___1_message, ___2_token, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
// System.IAsyncResult TheBackend.ToolKit.GoogleLogin.Android/GoogleLoginCallback::BeginInvoke(System.Boolean,System.String,System.String,System.AsyncCallback,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* GoogleLoginCallback_BeginInvoke_mD07B5BA332587AB8392F743DBA94FCB83DFE89C9 (GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* __this, bool ___0_isSuccess, String_t* ___1_message, String_t* ___2_token, AsyncCallback_t7FEF460CBDCFB9C5FA2EF776984778B9A4145F4C* ___3_callback, RuntimeObject* ___4_object, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	void *__d_args[4] = {0};
	__d_args[0] = Box(Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, &___0_isSuccess);
	__d_args[1] = ___1_message;
	__d_args[2] = ___2_token;
	return (RuntimeObject*)il2cpp_codegen_delegate_begin_invoke((RuntimeDelegate*)__this, __d_args, (RuntimeDelegate*)___3_callback, (RuntimeObject*)___4_object);
}
// System.Void TheBackend.ToolKit.GoogleLogin.Android/GoogleLoginCallback::EndInvoke(System.IAsyncResult)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GoogleLoginCallback_EndInvoke_m991F165DB7281764651AB8824CBF539628EA2934 (GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* __this, RuntimeObject* ___0_result, const RuntimeMethod* method) 
{
	il2cpp_codegen_delegate_end_invoke((Il2CppAsyncResult*) ___0_result, 0);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
void GoogleSignOutCallback_Invoke_mCB1EB05A50940E5E9BB2FF2CEC2A918C1EF2F281_Multicast(GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* __this, bool ___0_isSuccess, String_t* ___1_message, const RuntimeMethod* method)
{
	il2cpp_array_size_t length = __this->___delegates_13->max_length;
	Delegate_t** delegatesToInvoke = reinterpret_cast<Delegate_t**>(__this->___delegates_13->GetAddressAtUnchecked(0));
	for (il2cpp_array_size_t i = 0; i < length; i++)
	{
		GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* currentDelegate = reinterpret_cast<GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2*>(delegatesToInvoke[i]);
		typedef void (*FunctionPointerType) (RuntimeObject*, bool, String_t*, const RuntimeMethod*);
		((FunctionPointerType)currentDelegate->___invoke_impl_1)((Il2CppObject*)currentDelegate->___method_code_6, ___0_isSuccess, ___1_message, reinterpret_cast<RuntimeMethod*>(currentDelegate->___method_3));
	}
}
void GoogleSignOutCallback_Invoke_mCB1EB05A50940E5E9BB2FF2CEC2A918C1EF2F281_OpenInst(GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* __this, bool ___0_isSuccess, String_t* ___1_message, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (bool, String_t*, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr_0)(___0_isSuccess, ___1_message, method);
}
void GoogleSignOutCallback_Invoke_mCB1EB05A50940E5E9BB2FF2CEC2A918C1EF2F281_OpenStatic(GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* __this, bool ___0_isSuccess, String_t* ___1_message, const RuntimeMethod* method)
{
	typedef void (*FunctionPointerType) (bool, String_t*, const RuntimeMethod*);
	((FunctionPointerType)__this->___method_ptr_0)(___0_isSuccess, ___1_message, method);
}
void GoogleSignOutCallback_Invoke_mCB1EB05A50940E5E9BB2FF2CEC2A918C1EF2F281_OpenStaticInvoker(GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* __this, bool ___0_isSuccess, String_t* ___1_message, const RuntimeMethod* method)
{
	InvokerActionInvoker2< bool, String_t* >::Invoke((Il2CppMethodPointer)__this->___method_ptr_0, method, NULL, ___0_isSuccess, ___1_message);
}
void GoogleSignOutCallback_Invoke_mCB1EB05A50940E5E9BB2FF2CEC2A918C1EF2F281_ClosedStaticInvoker(GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* __this, bool ___0_isSuccess, String_t* ___1_message, const RuntimeMethod* method)
{
	InvokerActionInvoker3< RuntimeObject*, bool, String_t* >::Invoke((Il2CppMethodPointer)__this->___method_ptr_0, method, NULL, __this->___m_target_2, ___0_isSuccess, ___1_message);
}
IL2CPP_EXTERN_C  void DelegatePInvokeWrapper_GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2 (GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* __this, bool ___0_isSuccess, String_t* ___1_message, const RuntimeMethod* method)
{
	typedef void (DEFAULT_CALL *PInvokeFunc)(int32_t, char*);
	PInvokeFunc il2cppPInvokeFunc = reinterpret_cast<PInvokeFunc>(il2cpp_codegen_get_reverse_pinvoke_function_ptr(__this));
	// Marshaling of parameter '___1_message' to native representation
	char* ____1_message_marshaled = NULL;
	____1_message_marshaled = il2cpp_codegen_marshal_string(___1_message);

	// Native function invocation
	il2cppPInvokeFunc(static_cast<int32_t>(___0_isSuccess), ____1_message_marshaled);

	// Marshaling cleanup of parameter '___1_message' native representation
	il2cpp_codegen_marshal_free(____1_message_marshaled);
	____1_message_marshaled = NULL;

}
// System.Void TheBackend.ToolKit.GoogleLogin.Android/GoogleSignOutCallback::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GoogleSignOutCallback__ctor_mFFE5972C84393F228ACA57C1F6752AD9B40E238F (GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) 
{
	__this->___method_ptr_0 = (intptr_t)il2cpp_codegen_get_virtual_call_method_pointer((RuntimeMethod*)___1_method);
	__this->___method_3 = ___1_method;
	__this->___m_target_2 = ___0_object;
	Il2CppCodeGenWriteBarrier((void**)(&__this->___m_target_2), (void*)___0_object);
	int parameterCount = il2cpp_codegen_method_parameter_count((RuntimeMethod*)___1_method);
	__this->___method_code_6 = (intptr_t)__this;
	if (MethodIsStatic((RuntimeMethod*)___1_method))
	{
		bool isOpen = parameterCount == 2;
		if (il2cpp_codegen_call_method_via_invoker((RuntimeMethod*)___1_method))
			if (isOpen)
				__this->___invoke_impl_1 = (intptr_t)&GoogleSignOutCallback_Invoke_mCB1EB05A50940E5E9BB2FF2CEC2A918C1EF2F281_OpenStaticInvoker;
			else
				__this->___invoke_impl_1 = (intptr_t)&GoogleSignOutCallback_Invoke_mCB1EB05A50940E5E9BB2FF2CEC2A918C1EF2F281_ClosedStaticInvoker;
		else
			if (isOpen)
				__this->___invoke_impl_1 = (intptr_t)&GoogleSignOutCallback_Invoke_mCB1EB05A50940E5E9BB2FF2CEC2A918C1EF2F281_OpenStatic;
			else
				{
					__this->___invoke_impl_1 = __this->___method_ptr_0;
					__this->___method_code_6 = (intptr_t)__this->___m_target_2;
				}
	}
	else
	{
		if (___0_object == NULL)
			il2cpp_codegen_raise_exception(il2cpp_codegen_get_argument_exception(NULL, "Delegate to an instance method cannot have null 'this'."), NULL);
		__this->___invoke_impl_1 = __this->___method_ptr_0;
		__this->___method_code_6 = (intptr_t)__this->___m_target_2;
	}
	__this->___extra_arg_5 = (intptr_t)&GoogleSignOutCallback_Invoke_mCB1EB05A50940E5E9BB2FF2CEC2A918C1EF2F281_Multicast;
}
// System.Void TheBackend.ToolKit.GoogleLogin.Android/GoogleSignOutCallback::Invoke(System.Boolean,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GoogleSignOutCallback_Invoke_mCB1EB05A50940E5E9BB2FF2CEC2A918C1EF2F281 (GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* __this, bool ___0_isSuccess, String_t* ___1_message, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, bool, String_t*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___0_isSuccess, ___1_message, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
// System.IAsyncResult TheBackend.ToolKit.GoogleLogin.Android/GoogleSignOutCallback::BeginInvoke(System.Boolean,System.String,System.AsyncCallback,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* GoogleSignOutCallback_BeginInvoke_m6D03CC53A6E5DEAF22CE843A961EE05E44A34648 (GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* __this, bool ___0_isSuccess, String_t* ___1_message, AsyncCallback_t7FEF460CBDCFB9C5FA2EF776984778B9A4145F4C* ___2_callback, RuntimeObject* ___3_object, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	void *__d_args[3] = {0};
	__d_args[0] = Box(Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, &___0_isSuccess);
	__d_args[1] = ___1_message;
	return (RuntimeObject*)il2cpp_codegen_delegate_begin_invoke((RuntimeDelegate*)__this, __d_args, (RuntimeDelegate*)___2_callback, (RuntimeObject*)___3_object);
}
// System.Void TheBackend.ToolKit.GoogleLogin.Android/GoogleSignOutCallback::EndInvoke(System.IAsyncResult)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GoogleSignOutCallback_EndInvoke_mFF19DFE58C17ECF165A5FF67BDB0A2B713B41ADF (GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* __this, RuntimeObject* ___0_result, const RuntimeMethod* method) 
{
	il2cpp_codegen_delegate_end_invoke((Il2CppAsyncResult*) ___0_result, 0);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BackendOnUnityCallback__ctor_mCD40E5E8D5C513520B85E092426B369308D61E6A (BackendOnUnityCallback_tD10793DE59BC64A9A265FD049418D7CB3CD04362* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral774691F5E0813830196176DBCD276819181055E0);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_il2cpp_TypeInfo_var);
		AndroidJavaProxy__ctor_m2832886A0E1BBF6702653A7C6A4609F11FB712C7(__this, _stringLiteral774691F5E0813830196176DBCD276819181055E0, NULL);
		return;
	}
}
// System.Void TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback::onGetAccessToken(System.Boolean,System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BackendOnUnityCallback_onGetAccessToken_m4E9586DDDBC0941CDCDB9F52F103C0E4EFB34533 (BackendOnUnityCallback_tD10793DE59BC64A9A265FD049418D7CB3CD04362* __this, bool ___0_isSuccess, String_t* ___1_message, String_t* ___2_token, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaRunnable_tF23B9BBDA8C99A48BCEEA6335A47DA3C0EF34A7F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&String_t_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass1_0_U3ConGetAccessTokenU3Eb__0_mBFB387473369A2E1E04E453E8950D20523BB1CC6_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass1_0_tAEF0BA6A2A4D4C4291257AAECFC08872DE437A39_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral7D5D6BBF8281151C9F5F57DE5D5BABB7140A651D);
		s_Il2CppMethodInitialized = true;
	}
	U3CU3Ec__DisplayClass1_0_tAEF0BA6A2A4D4C4291257AAECFC08872DE437A39* V_0 = NULL;
	GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* G_B6_0 = NULL;
	GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* G_B5_0 = NULL;
	{
		U3CU3Ec__DisplayClass1_0_tAEF0BA6A2A4D4C4291257AAECFC08872DE437A39* L_0 = (U3CU3Ec__DisplayClass1_0_tAEF0BA6A2A4D4C4291257AAECFC08872DE437A39*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass1_0_tAEF0BA6A2A4D4C4291257AAECFC08872DE437A39_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__DisplayClass1_0__ctor_m20D9D45C0B43079063E3764D7124661EEBB62D47(L_0, NULL);
		V_0 = L_0;
		U3CU3Ec__DisplayClass1_0_tAEF0BA6A2A4D4C4291257AAECFC08872DE437A39* L_1 = V_0;
		bool L_2 = ___0_isSuccess;
		NullCheck(L_1);
		L_1->___isSuccess_0 = L_2;
		U3CU3Ec__DisplayClass1_0_tAEF0BA6A2A4D4C4291257AAECFC08872DE437A39* L_3 = V_0;
		String_t* L_4 = ___1_message;
		NullCheck(L_3);
		L_3->___message_1 = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&L_3->___message_1), (void*)L_4);
		U3CU3Ec__DisplayClass1_0_tAEF0BA6A2A4D4C4291257AAECFC08872DE437A39* L_5 = V_0;
		String_t* L_6 = ___2_token;
		NullCheck(L_5);
		L_5->___token_2 = L_6;
		Il2CppCodeGenWriteBarrier((void**)(&L_5->___token_2), (void*)L_6);
		U3CU3Ec__DisplayClass1_0_tAEF0BA6A2A4D4C4291257AAECFC08872DE437A39* L_7 = V_0;
		NullCheck(L_7);
		bool L_8 = L_7->___isSuccess_0;
		if (L_8)
		{
			goto IL_002e;
		}
	}
	{
		U3CU3Ec__DisplayClass1_0_tAEF0BA6A2A4D4C4291257AAECFC08872DE437A39* L_9 = V_0;
		String_t* L_10 = ((String_t_StaticFields*)il2cpp_codegen_static_fields_for(String_t_il2cpp_TypeInfo_var))->___Empty_6;
		NullCheck(L_9);
		L_9->___token_2 = L_10;
		Il2CppCodeGenWriteBarrier((void**)(&L_9->___token_2), (void*)L_10);
	}

IL_002e:
	{
		il2cpp_codegen_runtime_class_init_inline(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
		bool L_11 = ((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___isMainTreadCallback_8;
		if (!L_11)
		{
			goto IL_005a;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_12;
		L_12 = Android_get_PluginInstance_m917082A5EBA89BB894886E94C1D159080FDFBE17(NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_13 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)1);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_14 = L_13;
		U3CU3Ec__DisplayClass1_0_tAEF0BA6A2A4D4C4291257AAECFC08872DE437A39* L_15 = V_0;
		AndroidJavaRunnable_tF23B9BBDA8C99A48BCEEA6335A47DA3C0EF34A7F* L_16 = (AndroidJavaRunnable_tF23B9BBDA8C99A48BCEEA6335A47DA3C0EF34A7F*)il2cpp_codegen_object_new(AndroidJavaRunnable_tF23B9BBDA8C99A48BCEEA6335A47DA3C0EF34A7F_il2cpp_TypeInfo_var);
		NullCheck(L_16);
		AndroidJavaRunnable__ctor_m000E4FEB2DE8031A1CD733610D76E2BF60490334(L_16, L_15, (intptr_t)((void*)U3CU3Ec__DisplayClass1_0_U3ConGetAccessTokenU3Eb__0_mBFB387473369A2E1E04E453E8950D20523BB1CC6_RuntimeMethod_var), NULL);
		NullCheck(L_14);
		ArrayElementTypeCheck (L_14, L_16);
		(L_14)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_16);
		NullCheck(L_12);
		AndroidJavaObject_Call_mDEF7846E2AB1C5379069BB21049ED55A9D837B1C(L_12, _stringLiteral7D5D6BBF8281151C9F5F57DE5D5BABB7140A651D, L_14, NULL);
		return;
	}

IL_005a:
	{
		il2cpp_codegen_runtime_class_init_inline(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
		GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* L_17 = ((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___OnGoogleLogin_6;
		GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* L_18 = L_17;
		G_B5_0 = L_18;
		if (L_18)
		{
			G_B6_0 = L_18;
			goto IL_0064;
		}
	}
	{
		return;
	}

IL_0064:
	{
		U3CU3Ec__DisplayClass1_0_tAEF0BA6A2A4D4C4291257AAECFC08872DE437A39* L_19 = V_0;
		NullCheck(L_19);
		bool L_20 = L_19->___isSuccess_0;
		U3CU3Ec__DisplayClass1_0_tAEF0BA6A2A4D4C4291257AAECFC08872DE437A39* L_21 = V_0;
		NullCheck(L_21);
		String_t* L_22 = L_21->___message_1;
		U3CU3Ec__DisplayClass1_0_tAEF0BA6A2A4D4C4291257AAECFC08872DE437A39* L_23 = V_0;
		NullCheck(L_23);
		String_t* L_24 = L_23->___token_2;
		NullCheck(G_B6_0);
		GoogleLoginCallback_Invoke_mC58ED5473ED0705F936988D1EE2E874181E92411_inline(G_B6_0, L_20, L_22, L_24, NULL);
		return;
	}
}
// System.Void TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback::onGoogleSignOut(System.Boolean,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BackendOnUnityCallback_onGoogleSignOut_m13D76B2B9ADEEFC6E831E52C93987D9E1DCCD14C (BackendOnUnityCallback_tD10793DE59BC64A9A265FD049418D7CB3CD04362* __this, bool ___0_isSuccess, String_t* ___1_message, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaRunnable_tF23B9BBDA8C99A48BCEEA6335A47DA3C0EF34A7F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass2_0_U3ConGoogleSignOutU3Eb__0_m001A76F7B26BC0A5D0BADFF0B3B02F8808E45B8A_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass2_0_t34096128F462743F4ECF24F641BC27432EC60C01_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral7D5D6BBF8281151C9F5F57DE5D5BABB7140A651D);
		s_Il2CppMethodInitialized = true;
	}
	U3CU3Ec__DisplayClass2_0_t34096128F462743F4ECF24F641BC27432EC60C01* V_0 = NULL;
	GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* G_B4_0 = NULL;
	GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* G_B3_0 = NULL;
	{
		U3CU3Ec__DisplayClass2_0_t34096128F462743F4ECF24F641BC27432EC60C01* L_0 = (U3CU3Ec__DisplayClass2_0_t34096128F462743F4ECF24F641BC27432EC60C01*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass2_0_t34096128F462743F4ECF24F641BC27432EC60C01_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__DisplayClass2_0__ctor_mF838BA657A2E50DD03361E71F4856EAD8EFE8A41(L_0, NULL);
		V_0 = L_0;
		U3CU3Ec__DisplayClass2_0_t34096128F462743F4ECF24F641BC27432EC60C01* L_1 = V_0;
		bool L_2 = ___0_isSuccess;
		NullCheck(L_1);
		L_1->___isSuccess_0 = L_2;
		U3CU3Ec__DisplayClass2_0_t34096128F462743F4ECF24F641BC27432EC60C01* L_3 = V_0;
		String_t* L_4 = ___1_message;
		NullCheck(L_3);
		L_3->___message_1 = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&L_3->___message_1), (void*)L_4);
		il2cpp_codegen_runtime_class_init_inline(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
		bool L_5 = ((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___isMainTreadCallback_8;
		if (!L_5)
		{
			goto IL_0040;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_6;
		L_6 = Android_get_PluginInstance_m917082A5EBA89BB894886E94C1D159080FDFBE17(NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_7 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)1);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_8 = L_7;
		U3CU3Ec__DisplayClass2_0_t34096128F462743F4ECF24F641BC27432EC60C01* L_9 = V_0;
		AndroidJavaRunnable_tF23B9BBDA8C99A48BCEEA6335A47DA3C0EF34A7F* L_10 = (AndroidJavaRunnable_tF23B9BBDA8C99A48BCEEA6335A47DA3C0EF34A7F*)il2cpp_codegen_object_new(AndroidJavaRunnable_tF23B9BBDA8C99A48BCEEA6335A47DA3C0EF34A7F_il2cpp_TypeInfo_var);
		NullCheck(L_10);
		AndroidJavaRunnable__ctor_m000E4FEB2DE8031A1CD733610D76E2BF60490334(L_10, L_9, (intptr_t)((void*)U3CU3Ec__DisplayClass2_0_U3ConGoogleSignOutU3Eb__0_m001A76F7B26BC0A5D0BADFF0B3B02F8808E45B8A_RuntimeMethod_var), NULL);
		NullCheck(L_8);
		ArrayElementTypeCheck (L_8, L_10);
		(L_8)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_10);
		NullCheck(L_6);
		AndroidJavaObject_Call_mDEF7846E2AB1C5379069BB21049ED55A9D837B1C(L_6, _stringLiteral7D5D6BBF8281151C9F5F57DE5D5BABB7140A651D, L_8, NULL);
		return;
	}

IL_0040:
	{
		il2cpp_codegen_runtime_class_init_inline(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
		GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* L_11 = ((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___OnGoogleSignOut_7;
		GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* L_12 = L_11;
		G_B3_0 = L_12;
		if (L_12)
		{
			G_B4_0 = L_12;
			goto IL_004a;
		}
	}
	{
		return;
	}

IL_004a:
	{
		U3CU3Ec__DisplayClass2_0_t34096128F462743F4ECF24F641BC27432EC60C01* L_13 = V_0;
		NullCheck(L_13);
		bool L_14 = L_13->___isSuccess_0;
		U3CU3Ec__DisplayClass2_0_t34096128F462743F4ECF24F641BC27432EC60C01* L_15 = V_0;
		NullCheck(L_15);
		String_t* L_16 = L_15->___message_1;
		NullCheck(G_B4_0);
		GoogleSignOutCallback_Invoke_mCB1EB05A50940E5E9BB2FF2CEC2A918C1EF2F281_inline(G_B4_0, L_14, L_16, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback/<>c__DisplayClass1_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass1_0__ctor_m20D9D45C0B43079063E3764D7124661EEBB62D47 (U3CU3Ec__DisplayClass1_0_tAEF0BA6A2A4D4C4291257AAECFC08872DE437A39* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Void TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback/<>c__DisplayClass1_0::<onGetAccessToken>b__0()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass1_0_U3ConGetAccessTokenU3Eb__0_mBFB387473369A2E1E04E453E8950D20523BB1CC6 (U3CU3Ec__DisplayClass1_0_tAEF0BA6A2A4D4C4291257AAECFC08872DE437A39* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* G_B2_0 = NULL;
	GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* G_B1_0 = NULL;
	{
		il2cpp_codegen_runtime_class_init_inline(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
		GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* L_0 = ((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___OnGoogleLogin_6;
		GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* L_1 = L_0;
		G_B1_0 = L_1;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000a;
		}
	}
	{
		return;
	}

IL_000a:
	{
		bool L_2 = __this->___isSuccess_0;
		String_t* L_3 = __this->___message_1;
		String_t* L_4 = __this->___token_2;
		NullCheck(G_B2_0);
		GoogleLoginCallback_Invoke_mC58ED5473ED0705F936988D1EE2E874181E92411_inline(G_B2_0, L_2, L_3, L_4, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback/<>c__DisplayClass2_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass2_0__ctor_mF838BA657A2E50DD03361E71F4856EAD8EFE8A41 (U3CU3Ec__DisplayClass2_0_t34096128F462743F4ECF24F641BC27432EC60C01* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Void TheBackend.ToolKit.GoogleLogin.Android/BackendOnUnityCallback/<>c__DisplayClass2_0::<onGoogleSignOut>b__0()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass2_0_U3ConGoogleSignOutU3Eb__0_m001A76F7B26BC0A5D0BADFF0B3B02F8808E45B8A (U3CU3Ec__DisplayClass2_0_t34096128F462743F4ECF24F641BC27432EC60C01* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* G_B2_0 = NULL;
	GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* G_B1_0 = NULL;
	{
		il2cpp_codegen_runtime_class_init_inline(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var);
		GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* L_0 = ((Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_StaticFields*)il2cpp_codegen_static_fields_for(Android_t9F3D1894DDB8CAA0A466CD7ED852AC19E25DAAD0_il2cpp_TypeInfo_var))->___OnGoogleSignOut_7;
		GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* L_1 = L_0;
		G_B1_0 = L_1;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000a;
		}
	}
	{
		return;
	}

IL_000a:
	{
		bool L_2 = __this->___isSuccess_0;
		String_t* L_3 = __this->___message_1;
		NullCheck(G_B2_0);
		GoogleSignOutCallback_Invoke_mCB1EB05A50940E5E9BB2FF2CEC2A918C1EF2F281_inline(G_B2_0, L_2, L_3, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Version_tE426DB5655D0F22920AE16A2AA9AB7781B8255A7* AssemblyName_get_Version_mC20EC1E68FA7C40120112C2E29A19C9D948B5300_inline (AssemblyName_t555F1570F523D87D970C6E7F27B1B44C83EADDD2* __this, const RuntimeMethod* method) 
{
	{
		Version_tE426DB5655D0F22920AE16A2AA9AB7781B8255A7* L_0 = __this->___version_13;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void GoogleLoginCallback_Invoke_mC58ED5473ED0705F936988D1EE2E874181E92411_inline (GoogleLoginCallback_t7205CAA2B0F6C3E61B719B620C15E47B5A99DEB9* __this, bool ___0_isSuccess, String_t* ___1_message, String_t* ___2_token, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, bool, String_t*, String_t*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___0_isSuccess, ___1_message, ___2_token, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void GoogleSignOutCallback_Invoke_mCB1EB05A50940E5E9BB2FF2CEC2A918C1EF2F281_inline (GoogleSignOutCallback_t8BB805D2C832C9B2B75C71166FE133C07AE5E5F2* __this, bool ___0_isSuccess, String_t* ___1_message, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, bool, String_t*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___0_isSuccess, ___1_message, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_gshared_inline (const RuntimeMethod* method) 
{
	{
		il2cpp_codegen_runtime_class_init_inline(il2cpp_rgctx_data(method->rgctx_data, 0));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ((EmptyArray_1_tDF0DD7256B115243AA6BD5558417387A734240EE_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value_0;
		return L_0;
	}
}
