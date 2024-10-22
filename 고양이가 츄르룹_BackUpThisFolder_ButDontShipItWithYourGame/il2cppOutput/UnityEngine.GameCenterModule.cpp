#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>


template <typename T1, typename T2, typename T3>
struct InterfaceActionInvoker3
{
	typedef void (*Action)(void*, T1, T2, T3, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1, T2 p2, T3 p3)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, p1, p2, p3, invokeData.method);
	}
};
template <typename R>
struct InterfaceFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename R, typename T1>
struct InterfaceFuncInvoker1
{
	typedef R (*Func)(void*, T1, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		return ((Func)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};

// System.Action`1<System.Boolean>
struct Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C;
// System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.Achievement>
struct List_1_t5B1BB048CE74B294094EE23B5B5546279A93E60A;
// System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.AchievementDescription>
struct List_1_t3174CD866C458170B5DD174A25DD3D19A5C3B3B7;
// System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.Leaderboard>
struct List_1_tA7AA760D42FE7082707AC34B0D3327F25696E8D9;
// System.Collections.Generic.List`1<System.Object>
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D;
// System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.UserProfile>
struct List_1_tE6E66411E6B6A22232C67EAAEF4901A3486E55B3;
// UnityEngine.SocialPlatforms.Impl.Achievement[]
struct AchievementU5BU5D_tED830B37019AED404F90055C55FB9C9877735612;
// UnityEngine.SocialPlatforms.Impl.AchievementDescription[]
struct AchievementDescriptionU5BU5D_t6B3ED222FB06DD89115602C955A2CD98E000CCC5;
// System.Char[]
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
// System.Delegate[]
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
// UnityEngine.SocialPlatforms.IUserProfile[]
struct IUserProfileU5BU5D_t0179D2FF9BD9F78A4E0A10AE350DC1F19E5FCB43;
// System.Int32[]
struct Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C;
// UnityEngine.SocialPlatforms.Impl.Leaderboard[]
struct LeaderboardU5BU5D_tA96C54C7D9DD4377FCDDAD4AC15F58EB87E3EE85;
// System.Object[]
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
// System.String[]
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;
// UnityEngine.SocialPlatforms.Impl.UserProfile[]
struct UserProfileU5BU5D_tCE4194A0D6665FFF7943DDC3B0B9301D57F84A6A;
// UnityEngine.SocialPlatforms.Impl.Achievement
struct Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B;
// UnityEngine.SocialPlatforms.Impl.AchievementDescription
struct AchievementDescription_t0D2306DF6EE55C872DB06E6855D7B1AE0E6DDEF9;
// System.DelegateData
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
// System.Enum
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2;
// UnityEngine.SocialPlatforms.ILeaderboard
struct ILeaderboard_t62C479C3D29C80B4143F9EBA159EF2ECCA8D3C93;
// UnityEngine.SocialPlatforms.ILocalUser
struct ILocalUser_t8D7F34634AF940D34302E4F415AD50C7132EB7F6;
// UnityEngine.SocialPlatforms.ISocialPlatform
struct ISocialPlatform_tA236686987B4CB8A0694EEBAB4D7EB57CBABA254;
// UnityEngine.SocialPlatforms.IUserProfile
struct IUserProfile_tF23DFE5CF90A1020C168F5751F7C4F2B628E22E3;
// UnityEngine.SocialPlatforms.Impl.Leaderboard
struct Leaderboard_tBDB34CC6F79318BE6D7761015C70C8A5CC64EC71;
// UnityEngine.SocialPlatforms.Local
struct Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE;
// UnityEngine.SocialPlatforms.Impl.LocalUser
struct LocalUser_t55C68E98993F86B6FBB7A25F28EB989CD7E6A3AD;
// System.Reflection.MethodInfo
struct MethodInfo_t;
// System.String
struct String_t;
// UnityEngine.Texture2D
struct Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4;
// UnityEngine.SocialPlatforms.Impl.UserProfile
struct UserProfile_t3EF35349E23201EF9F3C5956C44384FA45C1EF29;
// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;

IL2CPP_EXTERN_C RuntimeClass* Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ActivePlatform_tA561A1F6671B5E621A6A1BB446EF480126BE2624_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ILocalUser_t8D7F34634AF940D34302E4F415AD50C7132EB7F6_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ISocialPlatform_tA236686987B4CB8A0694EEBAB4D7EB57CBABA254_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Leaderboard_tBDB34CC6F79318BE6D7761015C70C8A5CC64EC71_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* List_1_t3174CD866C458170B5DD174A25DD3D19A5C3B3B7_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* List_1_t5B1BB048CE74B294094EE23B5B5546279A93E60A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* List_1_tA7AA760D42FE7082707AC34B0D3327F25696E8D9_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* List_1_tE6E66411E6B6A22232C67EAAEF4901A3486E55B3_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* LocalUser_t55C68E98993F86B6FBB7A25F28EB989CD7E6A3AD_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* UserProfileU5BU5D_tCE4194A0D6665FFF7943DDC3B0B9301D57F84A6A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* UserState_t4C54A6F5CE00515F37F91DFB88AEB8FC56C6934C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral5ECA19F316C4BF30DC6AFDC7822F68EDF20BDA1E;
IL2CPP_EXTERN_C String_t* _stringLiteral697DAE71CF742F2932950075B1D260B4A087E7BA;
IL2CPP_EXTERN_C String_t* _stringLiteral960E5E7F211EFF3243DF14EDD1901DC9EF314D62;
IL2CPP_EXTERN_C String_t* _stringLiteralDFEE7C5599A1F9893EC0C7232CD714EAED23B5DF;
IL2CPP_EXTERN_C String_t* _stringLiteralF944DCD635F9801F7AC90A407FBC479964DEC024;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_Dispose_m3CE0C98650C97A387C0230CA6A1948E64CF21663_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_Dispose_m490686BC047B1AC93CE9E3578C23F565158A0BA2_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_MoveNext_m3B3FCF6D37A0383B2CE982B7F2B45A96C5CF1FDB_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_MoveNext_mDAEB3F275DBF0BC3DBE2000BF96C4B78CEBA5F3E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_get_Current_m13649A5037AD48DA1BC057B08E4298871F615C1B_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_get_Current_m9FC92E4A72F2ECB4C47FB9C4FC4A0BE9127ABC62_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Add_m9B223EE033DF80923355A3A50C6E8499761E5B12_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_GetEnumerator_m37CAAC69AD44E3877342547F60136218DDDD6BD1_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_GetEnumerator_mFF71F8F67EB8310BCD1AAEB4C669083B84A08494_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1__ctor_m00D08220BB0FBFE450F57049A4EEFA0898BDF022_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1__ctor_m23072805C6943D45A688FD6F11097CA8AFC14084_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1__ctor_m42603AF3F773C24EEBCCAF48DB31A596CCFBFFB3_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1__ctor_m59416295F911DEAE5FD7681CF4CD1FF1221370BE_RuntimeMethod_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;

struct IUserProfileU5BU5D_t0179D2FF9BD9F78A4E0A10AE350DC1F19E5FCB43;
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;
struct UserProfileU5BU5D_tCE4194A0D6665FFF7943DDC3B0B9301D57F84A6A;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// <Module>
struct U3CModuleU3E_t48670A4A557D138A8C338D0B799AEE0AB575434C 
{
};

// System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.Achievement>
struct List_1_t5B1BB048CE74B294094EE23B5B5546279A93E60A  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	AchievementU5BU5D_tED830B37019AED404F90055C55FB9C9877735612* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

// System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.AchievementDescription>
struct List_1_t3174CD866C458170B5DD174A25DD3D19A5C3B3B7  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	AchievementDescriptionU5BU5D_t6B3ED222FB06DD89115602C955A2CD98E000CCC5* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

// System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.Leaderboard>
struct List_1_tA7AA760D42FE7082707AC34B0D3327F25696E8D9  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	LeaderboardU5BU5D_tA96C54C7D9DD4377FCDDAD4AC15F58EB87E3EE85* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

// System.Collections.Generic.List`1<System.Object>
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

// System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.UserProfile>
struct List_1_tE6E66411E6B6A22232C67EAAEF4901A3486E55B3  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	UserProfileU5BU5D_tCE4194A0D6665FFF7943DDC3B0B9301D57F84A6A* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

// UnityEngine.SocialPlatforms.Impl.AchievementDescription
struct AchievementDescription_t0D2306DF6EE55C872DB06E6855D7B1AE0E6DDEF9  : public RuntimeObject
{
	// System.String UnityEngine.SocialPlatforms.Impl.AchievementDescription::<id>k__BackingField
	String_t* ___U3CidU3Ek__BackingField_0;
};

// UnityEngine.SocialPlatforms.ActivePlatform
struct ActivePlatform_tA561A1F6671B5E621A6A1BB446EF480126BE2624  : public RuntimeObject
{
};

// UnityEngine.SocialPlatforms.Local
struct Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE  : public RuntimeObject
{
	// System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.UserProfile> UnityEngine.SocialPlatforms.Local::m_Friends
	List_1_tE6E66411E6B6A22232C67EAAEF4901A3486E55B3* ___m_Friends_1;
	// System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.UserProfile> UnityEngine.SocialPlatforms.Local::m_Users
	List_1_tE6E66411E6B6A22232C67EAAEF4901A3486E55B3* ___m_Users_2;
	// System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.AchievementDescription> UnityEngine.SocialPlatforms.Local::m_AchievementDescriptions
	List_1_t3174CD866C458170B5DD174A25DD3D19A5C3B3B7* ___m_AchievementDescriptions_3;
	// System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.Achievement> UnityEngine.SocialPlatforms.Local::m_Achievements
	List_1_t5B1BB048CE74B294094EE23B5B5546279A93E60A* ___m_Achievements_4;
	// System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.Leaderboard> UnityEngine.SocialPlatforms.Local::m_Leaderboards
	List_1_tA7AA760D42FE7082707AC34B0D3327F25696E8D9* ___m_Leaderboards_5;
};

// UnityEngine.Social
struct Social_t81FE9092DC7E743150343FDD4F5F68103311491B  : public RuntimeObject
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

// UnityEngine.SocialPlatforms.Impl.UserProfile
struct UserProfile_t3EF35349E23201EF9F3C5956C44384FA45C1EF29  : public RuntimeObject
{
	// System.String UnityEngine.SocialPlatforms.Impl.UserProfile::m_UserName
	String_t* ___m_UserName_0;
	// System.String UnityEngine.SocialPlatforms.Impl.UserProfile::m_ID
	String_t* ___m_ID_1;
	// System.String UnityEngine.SocialPlatforms.Impl.UserProfile::m_legacyID
	String_t* ___m_legacyID_2;
	// System.Boolean UnityEngine.SocialPlatforms.Impl.UserProfile::m_IsFriend
	bool ___m_IsFriend_3;
	// UnityEngine.SocialPlatforms.UserState UnityEngine.SocialPlatforms.Impl.UserProfile::m_State
	int32_t ___m_State_4;
	// UnityEngine.Texture2D UnityEngine.SocialPlatforms.Impl.UserProfile::m_Image
	Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* ___m_Image_5;
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

// System.Collections.Generic.List`1/Enumerator<UnityEngine.SocialPlatforms.Impl.Achievement>
struct Enumerator_t4D84D59CEDFD06F975B6DB6EE89CF06A736E0C92 
{
	// System.Collections.Generic.List`1<T> System.Collections.Generic.List`1/Enumerator::_list
	List_1_t5B1BB048CE74B294094EE23B5B5546279A93E60A* ____list_0;
	// System.Int32 System.Collections.Generic.List`1/Enumerator::_index
	int32_t ____index_1;
	// System.Int32 System.Collections.Generic.List`1/Enumerator::_version
	int32_t ____version_2;
	// T System.Collections.Generic.List`1/Enumerator::_current
	Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* ____current_3;
};

// System.Collections.Generic.List`1/Enumerator<UnityEngine.SocialPlatforms.Impl.AchievementDescription>
struct Enumerator_tE79F4FF7BC35B8F2D2048E7B65A0C79D8F631261 
{
	// System.Collections.Generic.List`1<T> System.Collections.Generic.List`1/Enumerator::_list
	List_1_t3174CD866C458170B5DD174A25DD3D19A5C3B3B7* ____list_0;
	// System.Int32 System.Collections.Generic.List`1/Enumerator::_index
	int32_t ____index_1;
	// System.Int32 System.Collections.Generic.List`1/Enumerator::_version
	int32_t ____version_2;
	// T System.Collections.Generic.List`1/Enumerator::_current
	AchievementDescription_t0D2306DF6EE55C872DB06E6855D7B1AE0E6DDEF9* ____current_3;
};

// System.Collections.Generic.List`1/Enumerator<System.Object>
struct Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A 
{
	// System.Collections.Generic.List`1<T> System.Collections.Generic.List`1/Enumerator::_list
	List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* ____list_0;
	// System.Int32 System.Collections.Generic.List`1/Enumerator::_index
	int32_t ____index_1;
	// System.Int32 System.Collections.Generic.List`1/Enumerator::_version
	int32_t ____version_2;
	// T System.Collections.Generic.List`1/Enumerator::_current
	RuntimeObject* ____current_3;
};

// System.Boolean
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	// System.Boolean System.Boolean::m_value
	bool ___m_value_0;
};

// System.DateTime
struct DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D 
{
	// System.UInt64 System.DateTime::_dateData
	uint64_t ____dateData_46;
};

// System.Double
struct Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F 
{
	// System.Double System.Double::m_value
	double ___m_value_0;
};

// System.Enum
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2  : public ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F
{
};
// Native definition for P/Invoke marshalling of System.Enum
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.Enum
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_com
{
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

// UnityEngine.SocialPlatforms.Impl.LocalUser
struct LocalUser_t55C68E98993F86B6FBB7A25F28EB989CD7E6A3AD  : public UserProfile_t3EF35349E23201EF9F3C5956C44384FA45C1EF29
{
	// UnityEngine.SocialPlatforms.IUserProfile[] UnityEngine.SocialPlatforms.Impl.LocalUser::m_Friends
	IUserProfileU5BU5D_t0179D2FF9BD9F78A4E0A10AE350DC1F19E5FCB43* ___m_Friends_6;
	// System.Boolean UnityEngine.SocialPlatforms.Impl.LocalUser::m_Authenticated
	bool ___m_Authenticated_7;
	// System.Boolean UnityEngine.SocialPlatforms.Impl.LocalUser::m_Underage
	bool ___m_Underage_8;
};

// UnityEngine.SocialPlatforms.Range
struct Range_tDDBAD7CBDC5DD273DA4330F4E9CDF24C62F16ED6 
{
	// System.Int32 UnityEngine.SocialPlatforms.Range::from
	int32_t ___from_0;
	// System.Int32 UnityEngine.SocialPlatforms.Range::count
	int32_t ___count_1;
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

// UnityEngine.SocialPlatforms.Impl.Achievement
struct Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B  : public RuntimeObject
{
	// System.Boolean UnityEngine.SocialPlatforms.Impl.Achievement::m_Completed
	bool ___m_Completed_0;
	// System.Boolean UnityEngine.SocialPlatforms.Impl.Achievement::m_Hidden
	bool ___m_Hidden_1;
	// System.DateTime UnityEngine.SocialPlatforms.Impl.Achievement::m_LastReportedDate
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___m_LastReportedDate_2;
	// System.String UnityEngine.SocialPlatforms.Impl.Achievement::<id>k__BackingField
	String_t* ___U3CidU3Ek__BackingField_3;
	// System.Double UnityEngine.SocialPlatforms.Impl.Achievement::<percentCompleted>k__BackingField
	double ___U3CpercentCompletedU3Ek__BackingField_4;
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

// UnityEngine.SocialPlatforms.Impl.Leaderboard
struct Leaderboard_tBDB34CC6F79318BE6D7761015C70C8A5CC64EC71  : public RuntimeObject
{
	// System.String UnityEngine.SocialPlatforms.Impl.Leaderboard::<id>k__BackingField
	String_t* ___U3CidU3Ek__BackingField_0;
	// UnityEngine.SocialPlatforms.UserScope UnityEngine.SocialPlatforms.Impl.Leaderboard::<userScope>k__BackingField
	int32_t ___U3CuserScopeU3Ek__BackingField_1;
	// UnityEngine.SocialPlatforms.Range UnityEngine.SocialPlatforms.Impl.Leaderboard::<range>k__BackingField
	Range_tDDBAD7CBDC5DD273DA4330F4E9CDF24C62F16ED6 ___U3CrangeU3Ek__BackingField_2;
	// UnityEngine.SocialPlatforms.TimeScope UnityEngine.SocialPlatforms.Impl.Leaderboard::<timeScope>k__BackingField
	int32_t ___U3CtimeScopeU3Ek__BackingField_3;
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

// UnityEngine.Texture
struct Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};

// System.Action`1<System.Boolean>
struct Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C  : public MulticastDelegate_t
{
};

// UnityEngine.Texture2D
struct Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4  : public Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700
{
};

// <Module>

// <Module>

// System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.Achievement>
struct List_1_t5B1BB048CE74B294094EE23B5B5546279A93E60A_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	AchievementU5BU5D_tED830B37019AED404F90055C55FB9C9877735612* ___s_emptyArray_5;
};

// System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.Achievement>

// System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.AchievementDescription>
struct List_1_t3174CD866C458170B5DD174A25DD3D19A5C3B3B7_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	AchievementDescriptionU5BU5D_t6B3ED222FB06DD89115602C955A2CD98E000CCC5* ___s_emptyArray_5;
};

// System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.AchievementDescription>

// System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.Leaderboard>
struct List_1_tA7AA760D42FE7082707AC34B0D3327F25696E8D9_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	LeaderboardU5BU5D_tA96C54C7D9DD4377FCDDAD4AC15F58EB87E3EE85* ___s_emptyArray_5;
};

// System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.Leaderboard>

// System.Collections.Generic.List`1<System.Object>
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___s_emptyArray_5;
};

// System.Collections.Generic.List`1<System.Object>

// System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.UserProfile>
struct List_1_tE6E66411E6B6A22232C67EAAEF4901A3486E55B3_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	UserProfileU5BU5D_tCE4194A0D6665FFF7943DDC3B0B9301D57F84A6A* ___s_emptyArray_5;
};

// System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.UserProfile>

// UnityEngine.SocialPlatforms.Impl.AchievementDescription

// UnityEngine.SocialPlatforms.Impl.AchievementDescription

// UnityEngine.SocialPlatforms.ActivePlatform
struct ActivePlatform_tA561A1F6671B5E621A6A1BB446EF480126BE2624_StaticFields
{
	// UnityEngine.SocialPlatforms.ISocialPlatform UnityEngine.SocialPlatforms.ActivePlatform::_active
	RuntimeObject* ____active_0;
};

// UnityEngine.SocialPlatforms.ActivePlatform

// UnityEngine.SocialPlatforms.Local
struct Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE_StaticFields
{
	// UnityEngine.SocialPlatforms.Impl.LocalUser UnityEngine.SocialPlatforms.Local::m_LocalUser
	LocalUser_t55C68E98993F86B6FBB7A25F28EB989CD7E6A3AD* ___m_LocalUser_0;
};

// UnityEngine.SocialPlatforms.Local

// UnityEngine.Social

// UnityEngine.Social

// System.String
struct String_t_StaticFields
{
	// System.String System.String::Empty
	String_t* ___Empty_6;
};

// System.String

// UnityEngine.SocialPlatforms.Impl.UserProfile

// UnityEngine.SocialPlatforms.Impl.UserProfile

// System.Collections.Generic.List`1/Enumerator<UnityEngine.SocialPlatforms.Impl.Achievement>

// System.Collections.Generic.List`1/Enumerator<UnityEngine.SocialPlatforms.Impl.Achievement>

// System.Collections.Generic.List`1/Enumerator<UnityEngine.SocialPlatforms.Impl.AchievementDescription>

// System.Collections.Generic.List`1/Enumerator<UnityEngine.SocialPlatforms.Impl.AchievementDescription>

// System.Collections.Generic.List`1/Enumerator<System.Object>

// System.Collections.Generic.List`1/Enumerator<System.Object>

// System.Boolean
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	// System.String System.Boolean::TrueString
	String_t* ___TrueString_5;
	// System.String System.Boolean::FalseString
	String_t* ___FalseString_6;
};

// System.Boolean

// System.DateTime
struct DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D_StaticFields
{
	// System.Int32[] System.DateTime::s_daysToMonth365
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ___s_daysToMonth365_30;
	// System.Int32[] System.DateTime::s_daysToMonth366
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ___s_daysToMonth366_31;
	// System.DateTime System.DateTime::MinValue
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___MinValue_32;
	// System.DateTime System.DateTime::MaxValue
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___MaxValue_33;
	// System.DateTime System.DateTime::UnixEpoch
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___UnixEpoch_34;
};

// System.DateTime

// System.Double

// System.Double

// System.Enum
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_StaticFields
{
	// System.Char[] System.Enum::enumSeperatorCharArray
	CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ___enumSeperatorCharArray_0;
};

// System.Enum

// System.Int32

// System.Int32

// UnityEngine.SocialPlatforms.Impl.LocalUser

// UnityEngine.SocialPlatforms.Impl.LocalUser

// UnityEngine.SocialPlatforms.Range

// UnityEngine.SocialPlatforms.Range

// System.Void

// System.Void

// UnityEngine.SocialPlatforms.Impl.Achievement

// UnityEngine.SocialPlatforms.Impl.Achievement

// UnityEngine.SocialPlatforms.Impl.Leaderboard

// UnityEngine.SocialPlatforms.Impl.Leaderboard

// System.Action`1<System.Boolean>

// System.Action`1<System.Boolean>

// UnityEngine.Texture2D

// UnityEngine.Texture2D
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// UnityEngine.SocialPlatforms.IUserProfile[]
struct IUserProfileU5BU5D_t0179D2FF9BD9F78A4E0A10AE350DC1F19E5FCB43  : public RuntimeArray
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
// UnityEngine.SocialPlatforms.Impl.UserProfile[]
struct UserProfileU5BU5D_tCE4194A0D6665FFF7943DDC3B0B9301D57F84A6A  : public RuntimeArray
{
	ALIGN_FIELD (8) UserProfile_t3EF35349E23201EF9F3C5956C44384FA45C1EF29* m_Items[1];

	inline UserProfile_t3EF35349E23201EF9F3C5956C44384FA45C1EF29* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline UserProfile_t3EF35349E23201EF9F3C5956C44384FA45C1EF29** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, UserProfile_t3EF35349E23201EF9F3C5956C44384FA45C1EF29* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline UserProfile_t3EF35349E23201EF9F3C5956C44384FA45C1EF29* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline UserProfile_t3EF35349E23201EF9F3C5956C44384FA45C1EF29** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, UserProfile_t3EF35349E23201EF9F3C5956C44384FA45C1EF29* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
// System.String[]
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248  : public RuntimeArray
{
	ALIGN_FIELD (8) String_t* m_Items[1];

	inline String_t* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline String_t** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, String_t* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline String_t* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline String_t** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, String_t* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
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


// System.Collections.Generic.List`1/Enumerator<T> System.Collections.Generic.List`1<System.Object>::GetEnumerator()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A List_1_GetEnumerator_mD8294A7FA2BEB1929487127D476F8EC1CDC23BFC_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1/Enumerator<System.Object>::Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_mD9DC3E3C3697830A4823047AB29A77DBBB5ED419_gshared (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) ;
// T System.Collections.Generic.List`1/Enumerator<System.Object>::get_Current()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_gshared_inline (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) ;
// System.Void System.Action`1<System.Boolean>::Invoke(T)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_m69C8773D6967F3B224777183E24EA621CE056F8F_gshared_inline (Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* __this, bool ___0_obj, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.List`1/Enumerator<System.Object>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mE921CC8F29FBBDE7CC3209A0ED0D921D58D00BCB_gshared (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<System.Object>::Add(T)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<System.Object>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;

// UnityEngine.SocialPlatforms.ISocialPlatform UnityEngine.SocialPlatforms.ActivePlatform::get_Instance()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ActivePlatform_get_Instance_mC9DF8265897D79151D3F86D48A73691B6E3AFA06 (const RuntimeMethod* method) ;
// System.Void UnityEngine.SocialPlatforms.ActivePlatform::set_Instance(UnityEngine.SocialPlatforms.ISocialPlatform)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ActivePlatform_set_Instance_m11EC15E4BB9909B697B814DAA5C5C8314DE702E9 (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
// UnityEngine.SocialPlatforms.ISocialPlatform UnityEngine.Social::get_Active()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Social_get_Active_m54586B347AE1248C646891B24054E71C8DE5DC88 (const RuntimeMethod* method) ;
// System.Void UnityEngine.SocialPlatforms.Impl.LocalUser::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LocalUser__ctor_m6D2AE6DFC61CEC39842944D970E2B2B5547CBE97 (LocalUser_t55C68E98993F86B6FBB7A25F28EB989CD7E6A3AD* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.SocialPlatforms.Local::VerifyUser()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Local_VerifyUser_m8AAA9F80FAFDA28FF4026A1532ABD34A4CDD88A1 (Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE* __this, const RuntimeMethod* method) ;
// System.Collections.Generic.List`1/Enumerator<T> System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.Achievement>::GetEnumerator()
inline Enumerator_t4D84D59CEDFD06F975B6DB6EE89CF06A736E0C92 List_1_GetEnumerator_m37CAAC69AD44E3877342547F60136218DDDD6BD1 (List_1_t5B1BB048CE74B294094EE23B5B5546279A93E60A* __this, const RuntimeMethod* method)
{
	return ((  Enumerator_t4D84D59CEDFD06F975B6DB6EE89CF06A736E0C92 (*) (List_1_t5B1BB048CE74B294094EE23B5B5546279A93E60A*, const RuntimeMethod*))List_1_GetEnumerator_mD8294A7FA2BEB1929487127D476F8EC1CDC23BFC_gshared)(__this, method);
}
// System.Void System.Collections.Generic.List`1/Enumerator<UnityEngine.SocialPlatforms.Impl.Achievement>::Dispose()
inline void Enumerator_Dispose_m3CE0C98650C97A387C0230CA6A1948E64CF21663 (Enumerator_t4D84D59CEDFD06F975B6DB6EE89CF06A736E0C92* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t4D84D59CEDFD06F975B6DB6EE89CF06A736E0C92*, const RuntimeMethod*))Enumerator_Dispose_mD9DC3E3C3697830A4823047AB29A77DBBB5ED419_gshared)(__this, method);
}
// T System.Collections.Generic.List`1/Enumerator<UnityEngine.SocialPlatforms.Impl.Achievement>::get_Current()
inline Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* Enumerator_get_Current_m13649A5037AD48DA1BC057B08E4298871F615C1B_inline (Enumerator_t4D84D59CEDFD06F975B6DB6EE89CF06A736E0C92* __this, const RuntimeMethod* method)
{
	return ((  Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* (*) (Enumerator_t4D84D59CEDFD06F975B6DB6EE89CF06A736E0C92*, const RuntimeMethod*))Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_gshared_inline)(__this, method);
}
// System.String UnityEngine.SocialPlatforms.Impl.Achievement::get_id()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* Achievement_get_id_m0BDCED7ECE8BA170E619212CB2D23BD28322A0A6_inline (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, const RuntimeMethod* method) ;
// System.Boolean System.String::op_Equality(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1 (String_t* ___0_a, String_t* ___1_b, const RuntimeMethod* method) ;
// System.Double UnityEngine.SocialPlatforms.Impl.Achievement::get_percentCompleted()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR double Achievement_get_percentCompleted_mC0BFB768F39C110F2A851B5089C300E1C879D860_inline (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.SocialPlatforms.Impl.Achievement::SetCompleted(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Achievement_SetCompleted_mE342B402331E79D656CBC5E7FE881F69641D1E88 (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, bool ___0_value, const RuntimeMethod* method) ;
// System.Void UnityEngine.SocialPlatforms.Impl.Achievement::SetHidden(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Achievement_SetHidden_m9D0CA6D3F8539818DB88E3174AB57D4746B9CDCE (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, bool ___0_value, const RuntimeMethod* method) ;
// System.DateTime System.DateTime::get_Now()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D DateTime_get_Now_m636CB9651A9099D20BA1CF813A0C69637317325C (const RuntimeMethod* method) ;
// System.Void UnityEngine.SocialPlatforms.Impl.Achievement::SetLastReportedDate(System.DateTime)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Achievement_SetLastReportedDate_m0235AF958908983C7B025090C5A4061D27F7B3F3 (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___0_date, const RuntimeMethod* method) ;
// System.Void UnityEngine.SocialPlatforms.Impl.Achievement::set_percentCompleted(System.Double)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Achievement_set_percentCompleted_mFB33E191E8D7557E178EEC6DB80B616C268824BD_inline (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, double ___0_value, const RuntimeMethod* method) ;
// System.Void System.Action`1<System.Boolean>::Invoke(T)
inline void Action_1_Invoke_m69C8773D6967F3B224777183E24EA621CE056F8F_inline (Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* __this, bool ___0_obj, const RuntimeMethod* method)
{
	((  void (*) (Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C*, bool, const RuntimeMethod*))Action_1_Invoke_m69C8773D6967F3B224777183E24EA621CE056F8F_gshared_inline)(__this, ___0_obj, method);
}
// System.Boolean System.Collections.Generic.List`1/Enumerator<UnityEngine.SocialPlatforms.Impl.Achievement>::MoveNext()
inline bool Enumerator_MoveNext_m3B3FCF6D37A0383B2CE982B7F2B45A96C5CF1FDB (Enumerator_t4D84D59CEDFD06F975B6DB6EE89CF06A736E0C92* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t4D84D59CEDFD06F975B6DB6EE89CF06A736E0C92*, const RuntimeMethod*))Enumerator_MoveNext_mE921CC8F29FBBDE7CC3209A0ED0D921D58D00BCB_gshared)(__this, method);
}
// System.Collections.Generic.List`1/Enumerator<T> System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.AchievementDescription>::GetEnumerator()
inline Enumerator_tE79F4FF7BC35B8F2D2048E7B65A0C79D8F631261 List_1_GetEnumerator_mFF71F8F67EB8310BCD1AAEB4C669083B84A08494 (List_1_t3174CD866C458170B5DD174A25DD3D19A5C3B3B7* __this, const RuntimeMethod* method)
{
	return ((  Enumerator_tE79F4FF7BC35B8F2D2048E7B65A0C79D8F631261 (*) (List_1_t3174CD866C458170B5DD174A25DD3D19A5C3B3B7*, const RuntimeMethod*))List_1_GetEnumerator_mD8294A7FA2BEB1929487127D476F8EC1CDC23BFC_gshared)(__this, method);
}
// System.Void System.Collections.Generic.List`1/Enumerator<UnityEngine.SocialPlatforms.Impl.AchievementDescription>::Dispose()
inline void Enumerator_Dispose_m490686BC047B1AC93CE9E3578C23F565158A0BA2 (Enumerator_tE79F4FF7BC35B8F2D2048E7B65A0C79D8F631261* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tE79F4FF7BC35B8F2D2048E7B65A0C79D8F631261*, const RuntimeMethod*))Enumerator_Dispose_mD9DC3E3C3697830A4823047AB29A77DBBB5ED419_gshared)(__this, method);
}
// T System.Collections.Generic.List`1/Enumerator<UnityEngine.SocialPlatforms.Impl.AchievementDescription>::get_Current()
inline AchievementDescription_t0D2306DF6EE55C872DB06E6855D7B1AE0E6DDEF9* Enumerator_get_Current_m9FC92E4A72F2ECB4C47FB9C4FC4A0BE9127ABC62_inline (Enumerator_tE79F4FF7BC35B8F2D2048E7B65A0C79D8F631261* __this, const RuntimeMethod* method)
{
	return ((  AchievementDescription_t0D2306DF6EE55C872DB06E6855D7B1AE0E6DDEF9* (*) (Enumerator_tE79F4FF7BC35B8F2D2048E7B65A0C79D8F631261*, const RuntimeMethod*))Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_gshared_inline)(__this, method);
}
// System.String UnityEngine.SocialPlatforms.Impl.AchievementDescription::get_id()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* AchievementDescription_get_id_mC954988F8344E3B7E316D15EBD240A85D06A77C7_inline (AchievementDescription_t0D2306DF6EE55C872DB06E6855D7B1AE0E6DDEF9* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.SocialPlatforms.Impl.Achievement::.ctor(System.String,System.Double,System.Boolean,System.Boolean,System.DateTime)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Achievement__ctor_mE4B26ACD8E4220F645864036B7F9AD2E7D746B85 (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, String_t* ___0_id, double ___1_percentCompleted, bool ___2_completed, bool ___3_hidden, DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___4_lastReportedDate, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.Achievement>::Add(T)
inline void List_1_Add_m9B223EE033DF80923355A3A50C6E8499761E5B12_inline (List_1_t5B1BB048CE74B294094EE23B5B5546279A93E60A* __this, Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* ___0_item, const RuntimeMethod* method)
{
	((  void (*) (List_1_t5B1BB048CE74B294094EE23B5B5546279A93E60A*, Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B*, const RuntimeMethod*))List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline)(__this, ___0_item, method);
}
// System.Boolean System.Collections.Generic.List`1/Enumerator<UnityEngine.SocialPlatforms.Impl.AchievementDescription>::MoveNext()
inline bool Enumerator_MoveNext_mDAEB3F275DBF0BC3DBE2000BF96C4B78CEBA5F3E (Enumerator_tE79F4FF7BC35B8F2D2048E7B65A0C79D8F631261* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_tE79F4FF7BC35B8F2D2048E7B65A0C79D8F631261*, const RuntimeMethod*))Enumerator_MoveNext_mE921CC8F29FBBDE7CC3209A0ED0D921D58D00BCB_gshared)(__this, method);
}
// System.Void UnityEngine.Debug::LogError(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2 (RuntimeObject* ___0_message, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.SocialPlatforms.Impl.Leaderboard::get_loading()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Leaderboard_get_loading_mE6CC3F9AE66A0909F02F3EE8BBCD8DA9B4BB8A39 (Leaderboard_tBDB34CC6F79318BE6D7761015C70C8A5CC64EC71* __this, const RuntimeMethod* method) ;
// UnityEngine.SocialPlatforms.ILocalUser UnityEngine.SocialPlatforms.Local::get_localUser()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Local_get_localUser_m21888B1B23DBA198D651B239D51DBCA525474F9E (Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.UserProfile>::.ctor()
inline void List_1__ctor_m59416295F911DEAE5FD7681CF4CD1FF1221370BE (List_1_tE6E66411E6B6A22232C67EAAEF4901A3486E55B3* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_tE6E66411E6B6A22232C67EAAEF4901A3486E55B3*, const RuntimeMethod*))List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared)(__this, method);
}
// System.Void System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.AchievementDescription>::.ctor()
inline void List_1__ctor_m00D08220BB0FBFE450F57049A4EEFA0898BDF022 (List_1_t3174CD866C458170B5DD174A25DD3D19A5C3B3B7* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_t3174CD866C458170B5DD174A25DD3D19A5C3B3B7*, const RuntimeMethod*))List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared)(__this, method);
}
// System.Void System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.Achievement>::.ctor()
inline void List_1__ctor_m42603AF3F773C24EEBCCAF48DB31A596CCFBFFB3 (List_1_t5B1BB048CE74B294094EE23B5B5546279A93E60A* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_t5B1BB048CE74B294094EE23B5B5546279A93E60A*, const RuntimeMethod*))List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared)(__this, method);
}
// System.Void System.Collections.Generic.List`1<UnityEngine.SocialPlatforms.Impl.Leaderboard>::.ctor()
inline void List_1__ctor_m23072805C6943D45A688FD6F11097CA8AFC14084 (List_1_tA7AA760D42FE7082707AC34B0D3327F25696E8D9* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_tA7AA760D42FE7082707AC34B0D3327F25696E8D9*, const RuntimeMethod*))List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared)(__this, method);
}
// System.Void System.Object::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
// UnityEngine.SocialPlatforms.ISocialPlatform UnityEngine.SocialPlatforms.ActivePlatform::SelectSocialPlatform()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ActivePlatform_SelectSocialPlatform_mEA0704F85B0FD046DE2E4069B1EC413BD835BCA7 (const RuntimeMethod* method) ;
// System.Void UnityEngine.SocialPlatforms.Local::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Local__ctor_m223523E0079C7C184D1B804092E5C0A61B0E110E (Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.SocialPlatforms.Impl.UserProfile::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UserProfile__ctor_m12A26F8BDE41F4B55A645BB1D4038E81A877E680 (UserProfile_t3EF35349E23201EF9F3C5956C44384FA45C1EF29* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.Texture2D::.ctor(System.Int32,System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Texture2D__ctor_m3BA82E87442B7F69E118477069AE11101B9DF796 (Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* __this, int32_t ___0_width, int32_t ___1_height, const RuntimeMethod* method) ;
// System.String UnityEngine.SocialPlatforms.Impl.UserProfile::get_id()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* UserProfile_get_id_m16A4060A0C7E4480F68D6915E6FAB15EAE973336 (UserProfile_t3EF35349E23201EF9F3C5956C44384FA45C1EF29* __this, const RuntimeMethod* method) ;
// System.String UnityEngine.SocialPlatforms.Impl.UserProfile::get_userName()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* UserProfile_get_userName_mDAAF12B06B939DDAAB6F10E8CB40B21C48A94F30 (UserProfile_t3EF35349E23201EF9F3C5956C44384FA45C1EF29* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.SocialPlatforms.Impl.UserProfile::get_isFriend()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool UserProfile_get_isFriend_m353D113C22BFA80F0E9A1DBEC40E4EF4984AC4EC (UserProfile_t3EF35349E23201EF9F3C5956C44384FA45C1EF29* __this, const RuntimeMethod* method) ;
// System.String System.Boolean::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Boolean_ToString_m6646C8026B1DF381A1EE8CD13549175E9703CC63 (bool* __this, const RuntimeMethod* method) ;
// UnityEngine.SocialPlatforms.UserState UnityEngine.SocialPlatforms.Impl.UserProfile::get_state()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t UserProfile_get_state_mF5F8CF4E71CD46ADBCC58E5A3AFA715B3E5F9D4A (UserProfile_t3EF35349E23201EF9F3C5956C44384FA45C1EF29* __this, const RuntimeMethod* method) ;
// System.String System.Enum::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Enum_ToString_m946B0B83C4470457D0FF555D862022C72BB55741 (RuntimeObject* __this, const RuntimeMethod* method) ;
// System.String System.String::Concat(System.String[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m647EBF831F54B6DF7D5AFA5FD012CF4EE7571B6A (StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* ___0_values, const RuntimeMethod* method) ;
// System.Void UnityEngine.SocialPlatforms.Impl.Achievement::set_id(System.String)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Achievement_set_id_mF2FB90F3D2F1F38DBDB449C956B8B35BAF6108EE_inline (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, String_t* ___0_value, const RuntimeMethod* method) ;
// System.String System.Double::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Double_ToString_m7499A5D792419537DCB9470A3675CEF5117DE339 (double* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.SocialPlatforms.Impl.Achievement::get_completed()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Achievement_get_completed_m5A625BD69E129E468D83A98CF4CAA4AA28E0B2BF (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.SocialPlatforms.Impl.Achievement::get_hidden()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Achievement_get_hidden_m343030FB2BE6636313A09DEDCB02BE0ADB5E3A8E (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, const RuntimeMethod* method) ;
// System.DateTime UnityEngine.SocialPlatforms.Impl.Achievement::get_lastReportedDate()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D Achievement_get_lastReportedDate_mEACB3BADB0996AF967B26378FA48BC964E68F091 (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, const RuntimeMethod* method) ;
// System.String System.DateTime::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* DateTime_ToString_m447C83E1F8FFFFF4D20C0F7D5C18DEB160F9833A (DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D* __this, const RuntimeMethod* method) ;
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
// UnityEngine.SocialPlatforms.ISocialPlatform UnityEngine.Social::get_Active()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Social_get_Active_m54586B347AE1248C646891B24054E71C8DE5DC88 (const RuntimeMethod* method) 
{
	RuntimeObject* V_0 = NULL;
	{
		RuntimeObject* L_0;
		L_0 = ActivePlatform_get_Instance_mC9DF8265897D79151D3F86D48A73691B6E3AFA06(NULL);
		V_0 = L_0;
		goto IL_0009;
	}

IL_0009:
	{
		RuntimeObject* L_1 = V_0;
		return L_1;
	}
}
// System.Void UnityEngine.Social::set_Active(UnityEngine.SocialPlatforms.ISocialPlatform)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Social_set_Active_m3E079BA4CFB75E910F913A8477135C0282BC72FB (RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = ___0_value;
		ActivePlatform_set_Instance_m11EC15E4BB9909B697B814DAA5C5C8314DE702E9(L_0, NULL);
		return;
	}
}
// System.Void UnityEngine.Social::ReportProgress(System.String,System.Double,System.Action`1<System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Social_ReportProgress_m3BA1C646169495DA260682E4C0ED8217C406A85E (String_t* ___0_achievementID, double ___1_progress, Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* ___2_callback, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ISocialPlatform_tA236686987B4CB8A0694EEBAB4D7EB57CBABA254_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		RuntimeObject* L_0;
		L_0 = Social_get_Active_m54586B347AE1248C646891B24054E71C8DE5DC88(NULL);
		String_t* L_1 = ___0_achievementID;
		double L_2 = ___1_progress;
		Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* L_3 = ___2_callback;
		NullCheck(L_0);
		InterfaceActionInvoker3< String_t*, double, Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* >::Invoke(0 /* System.Void UnityEngine.SocialPlatforms.ISocialPlatform::ReportProgress(System.String,System.Double,System.Action`1<System.Boolean>) */, ISocialPlatform_tA236686987B4CB8A0694EEBAB4D7EB57CBABA254_il2cpp_TypeInfo_var, L_0, L_1, L_2, L_3);
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
// UnityEngine.SocialPlatforms.ILocalUser UnityEngine.SocialPlatforms.Local::get_localUser()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Local_get_localUser_m21888B1B23DBA198D651B239D51DBCA525474F9E (Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&LocalUser_t55C68E98993F86B6FBB7A25F28EB989CD7E6A3AD_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	RuntimeObject* V_1 = NULL;
	{
		LocalUser_t55C68E98993F86B6FBB7A25F28EB989CD7E6A3AD* L_0 = ((Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE_StaticFields*)il2cpp_codegen_static_fields_for(Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE_il2cpp_TypeInfo_var))->___m_LocalUser_0;
		V_0 = (bool)((((RuntimeObject*)(LocalUser_t55C68E98993F86B6FBB7A25F28EB989CD7E6A3AD*)L_0) == ((RuntimeObject*)(RuntimeObject*)NULL))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_0017;
		}
	}
	{
		LocalUser_t55C68E98993F86B6FBB7A25F28EB989CD7E6A3AD* L_2 = (LocalUser_t55C68E98993F86B6FBB7A25F28EB989CD7E6A3AD*)il2cpp_codegen_object_new(LocalUser_t55C68E98993F86B6FBB7A25F28EB989CD7E6A3AD_il2cpp_TypeInfo_var);
		NullCheck(L_2);
		LocalUser__ctor_m6D2AE6DFC61CEC39842944D970E2B2B5547CBE97(L_2, NULL);
		((Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE_StaticFields*)il2cpp_codegen_static_fields_for(Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE_il2cpp_TypeInfo_var))->___m_LocalUser_0 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&((Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE_StaticFields*)il2cpp_codegen_static_fields_for(Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE_il2cpp_TypeInfo_var))->___m_LocalUser_0), (void*)L_2);
	}

IL_0017:
	{
		LocalUser_t55C68E98993F86B6FBB7A25F28EB989CD7E6A3AD* L_3 = ((Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE_StaticFields*)il2cpp_codegen_static_fields_for(Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE_il2cpp_TypeInfo_var))->___m_LocalUser_0;
		V_1 = L_3;
		goto IL_001f;
	}

IL_001f:
	{
		RuntimeObject* L_4 = V_1;
		return L_4;
	}
}
// System.Void UnityEngine.SocialPlatforms.Local::ReportProgress(System.String,System.Double,System.Action`1<System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Local_ReportProgress_mC145E69B11D5093F1F6A3FC62F1EBEC901994310 (Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE* __this, String_t* ___0_id, double ___1_progress, Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* ___2_callback, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m3CE0C98650C97A387C0230CA6A1948E64CF21663_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m490686BC047B1AC93CE9E3578C23F565158A0BA2_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_m3B3FCF6D37A0383B2CE982B7F2B45A96C5CF1FDB_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mDAEB3F275DBF0BC3DBE2000BF96C4B78CEBA5F3E_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_m13649A5037AD48DA1BC057B08E4298871F615C1B_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_m9FC92E4A72F2ECB4C47FB9C4FC4A0BE9127ABC62_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Add_m9B223EE033DF80923355A3A50C6E8499761E5B12_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m37CAAC69AD44E3877342547F60136218DDDD6BD1_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_mFF71F8F67EB8310BCD1AAEB4C669083B84A08494_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralDFEE7C5599A1F9893EC0C7232CD714EAED23B5DF);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	Enumerator_t4D84D59CEDFD06F975B6DB6EE89CF06A736E0C92 V_1;
	memset((&V_1), 0, sizeof(V_1));
	Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* V_2 = NULL;
	bool V_3 = false;
	bool V_4 = false;
	bool V_5 = false;
	Enumerator_tE79F4FF7BC35B8F2D2048E7B65A0C79D8F631261 V_6;
	memset((&V_6), 0, sizeof(V_6));
	AchievementDescription_t0D2306DF6EE55C872DB06E6855D7B1AE0E6DDEF9* V_7 = NULL;
	bool V_8 = false;
	bool V_9 = false;
	Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* V_10 = NULL;
	bool V_11 = false;
	bool V_12 = false;
	int32_t G_B7_0 = 0;
	{
		bool L_0;
		L_0 = Local_VerifyUser_m8AAA9F80FAFDA28FF4026A1532ABD34A4CDD88A1(__this, NULL);
		V_0 = (bool)((((int32_t)L_0) == ((int32_t)0))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_0013;
		}
	}
	{
		goto IL_0165;
	}

IL_0013:
	{
		List_1_t5B1BB048CE74B294094EE23B5B5546279A93E60A* L_2 = __this->___m_Achievements_4;
		NullCheck(L_2);
		Enumerator_t4D84D59CEDFD06F975B6DB6EE89CF06A736E0C92 L_3;
		L_3 = List_1_GetEnumerator_m37CAAC69AD44E3877342547F60136218DDDD6BD1(L_2, List_1_GetEnumerator_m37CAAC69AD44E3877342547F60136218DDDD6BD1_RuntimeMethod_var);
		V_1 = L_3;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_00ac:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m3CE0C98650C97A387C0230CA6A1948E64CF21663((&V_1), Enumerator_Dispose_m3CE0C98650C97A387C0230CA6A1948E64CF21663_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_009e_1;
			}

IL_0022_1:
			{
				Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* L_4;
				L_4 = Enumerator_get_Current_m13649A5037AD48DA1BC057B08E4298871F615C1B_inline((&V_1), Enumerator_get_Current_m13649A5037AD48DA1BC057B08E4298871F615C1B_RuntimeMethod_var);
				V_2 = L_4;
				Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* L_5 = V_2;
				NullCheck(L_5);
				String_t* L_6;
				L_6 = Achievement_get_id_m0BDCED7ECE8BA170E619212CB2D23BD28322A0A6_inline(L_5, NULL);
				String_t* L_7 = ___0_id;
				bool L_8;
				L_8 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_6, L_7, NULL);
				if (!L_8)
				{
					goto IL_0047_1;
				}
			}
			{
				Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* L_9 = V_2;
				NullCheck(L_9);
				double L_10;
				L_10 = Achievement_get_percentCompleted_mC0BFB768F39C110F2A851B5089C300E1C879D860_inline(L_9, NULL);
				double L_11 = ___1_progress;
				G_B7_0 = ((((int32_t)((!(((double)L_10) <= ((double)L_11)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
				goto IL_0048_1;
			}

IL_0047_1:
			{
				G_B7_0 = 0;
			}

IL_0048_1:
			{
				V_3 = (bool)G_B7_0;
				bool L_12 = V_3;
				if (!L_12)
				{
					goto IL_009d_1;
				}
			}
			{
				double L_13 = ___1_progress;
				V_4 = (bool)((((int32_t)((!(((double)L_13) >= ((double)(100.0))))? 1 : 0)) == ((int32_t)0))? 1 : 0);
				bool L_14 = V_4;
				if (!L_14)
				{
					goto IL_006a_1;
				}
			}
			{
				Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* L_15 = V_2;
				NullCheck(L_15);
				Achievement_SetCompleted_mE342B402331E79D656CBC5E7FE881F69641D1E88(L_15, (bool)1, NULL);
			}

IL_006a_1:
			{
				Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* L_16 = V_2;
				NullCheck(L_16);
				Achievement_SetHidden_m9D0CA6D3F8539818DB88E3174AB57D4746B9CDCE(L_16, (bool)0, NULL);
				Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* L_17 = V_2;
				il2cpp_codegen_runtime_class_init_inline(DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D_il2cpp_TypeInfo_var);
				DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D L_18;
				L_18 = DateTime_get_Now_m636CB9651A9099D20BA1CF813A0C69637317325C(NULL);
				NullCheck(L_17);
				Achievement_SetLastReportedDate_m0235AF958908983C7B025090C5A4061D27F7B3F3(L_17, L_18, NULL);
				Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* L_19 = V_2;
				double L_20 = ___1_progress;
				NullCheck(L_19);
				Achievement_set_percentCompleted_mFB33E191E8D7557E178EEC6DB80B616C268824BD_inline(L_19, L_20, NULL);
				Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* L_21 = ___2_callback;
				V_5 = (bool)((!(((RuntimeObject*)(Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C*)L_21) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
				bool L_22 = V_5;
				if (!L_22)
				{
					goto IL_0098_1;
				}
			}
			{
				Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* L_23 = ___2_callback;
				NullCheck(L_23);
				Action_1_Invoke_m69C8773D6967F3B224777183E24EA621CE056F8F_inline(L_23, (bool)1, NULL);
			}

IL_0098_1:
			{
				goto IL_0165;
			}

IL_009d_1:
			{
			}

IL_009e_1:
			{
				bool L_24;
				L_24 = Enumerator_MoveNext_m3B3FCF6D37A0383B2CE982B7F2B45A96C5CF1FDB((&V_1), Enumerator_MoveNext_m3B3FCF6D37A0383B2CE982B7F2B45A96C5CF1FDB_RuntimeMethod_var);
				if (L_24)
				{
					goto IL_0022_1;
				}
			}
			{
				goto IL_00bb;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_00bb:
	{
		List_1_t3174CD866C458170B5DD174A25DD3D19A5C3B3B7* L_25 = __this->___m_AchievementDescriptions_3;
		NullCheck(L_25);
		Enumerator_tE79F4FF7BC35B8F2D2048E7B65A0C79D8F631261 L_26;
		L_26 = List_1_GetEnumerator_mFF71F8F67EB8310BCD1AAEB4C669083B84A08494(L_25, List_1_GetEnumerator_mFF71F8F67EB8310BCD1AAEB4C669083B84A08494_RuntimeMethod_var);
		V_6 = L_26;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0139:
			{// begin finally (depth: 1)
				Enumerator_Dispose_m490686BC047B1AC93CE9E3578C23F565158A0BA2((&V_6), Enumerator_Dispose_m490686BC047B1AC93CE9E3578C23F565158A0BA2_RuntimeMethod_var);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_012e_1;
			}

IL_00cb_1:
			{
				AchievementDescription_t0D2306DF6EE55C872DB06E6855D7B1AE0E6DDEF9* L_27;
				L_27 = Enumerator_get_Current_m9FC92E4A72F2ECB4C47FB9C4FC4A0BE9127ABC62_inline((&V_6), Enumerator_get_Current_m9FC92E4A72F2ECB4C47FB9C4FC4A0BE9127ABC62_RuntimeMethod_var);
				V_7 = L_27;
				AchievementDescription_t0D2306DF6EE55C872DB06E6855D7B1AE0E6DDEF9* L_28 = V_7;
				NullCheck(L_28);
				String_t* L_29;
				L_29 = AchievementDescription_get_id_mC954988F8344E3B7E316D15EBD240A85D06A77C7_inline(L_28, NULL);
				String_t* L_30 = ___0_id;
				bool L_31;
				L_31 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_29, L_30, NULL);
				V_8 = L_31;
				bool L_32 = V_8;
				if (!L_32)
				{
					goto IL_012d_1;
				}
			}
			{
				double L_33 = ___1_progress;
				V_9 = (bool)((((int32_t)((!(((double)L_33) >= ((double)(100.0))))? 1 : 0)) == ((int32_t)0))? 1 : 0);
				String_t* L_34 = ___0_id;
				double L_35 = ___1_progress;
				bool L_36 = V_9;
				il2cpp_codegen_runtime_class_init_inline(DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D_il2cpp_TypeInfo_var);
				DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D L_37;
				L_37 = DateTime_get_Now_m636CB9651A9099D20BA1CF813A0C69637317325C(NULL);
				Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* L_38 = (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B*)il2cpp_codegen_object_new(Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B_il2cpp_TypeInfo_var);
				NullCheck(L_38);
				Achievement__ctor_mE4B26ACD8E4220F645864036B7F9AD2E7D746B85(L_38, L_34, L_35, L_36, (bool)0, L_37, NULL);
				V_10 = L_38;
				List_1_t5B1BB048CE74B294094EE23B5B5546279A93E60A* L_39 = __this->___m_Achievements_4;
				Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* L_40 = V_10;
				NullCheck(L_39);
				List_1_Add_m9B223EE033DF80923355A3A50C6E8499761E5B12_inline(L_39, L_40, List_1_Add_m9B223EE033DF80923355A3A50C6E8499761E5B12_RuntimeMethod_var);
				Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* L_41 = ___2_callback;
				V_11 = (bool)((!(((RuntimeObject*)(Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C*)L_41) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
				bool L_42 = V_11;
				if (!L_42)
				{
					goto IL_012b_1;
				}
			}
			{
				Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* L_43 = ___2_callback;
				NullCheck(L_43);
				Action_1_Invoke_m69C8773D6967F3B224777183E24EA621CE056F8F_inline(L_43, (bool)1, NULL);
			}

IL_012b_1:
			{
				goto IL_0165;
			}

IL_012d_1:
			{
			}

IL_012e_1:
			{
				bool L_44;
				L_44 = Enumerator_MoveNext_mDAEB3F275DBF0BC3DBE2000BF96C4B78CEBA5F3E((&V_6), Enumerator_MoveNext_mDAEB3F275DBF0BC3DBE2000BF96C4B78CEBA5F3E_RuntimeMethod_var);
				if (L_44)
				{
					goto IL_00cb_1;
				}
			}
			{
				goto IL_0148;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0148:
	{
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(_stringLiteralDFEE7C5599A1F9893EC0C7232CD714EAED23B5DF, NULL);
		Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* L_45 = ___2_callback;
		V_12 = (bool)((!(((RuntimeObject*)(Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C*)L_45) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		bool L_46 = V_12;
		if (!L_46)
		{
			goto IL_0165;
		}
	}
	{
		Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* L_47 = ___2_callback;
		NullCheck(L_47);
		Action_1_Invoke_m69C8773D6967F3B224777183E24EA621CE056F8F_inline(L_47, (bool)0, NULL);
	}

IL_0165:
	{
		return;
	}
}
// System.Boolean UnityEngine.SocialPlatforms.Local::UnityEngine.SocialPlatforms.ISocialPlatform.GetLoading(UnityEngine.SocialPlatforms.ILeaderboard)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Local_UnityEngine_SocialPlatforms_ISocialPlatform_GetLoading_m64E02B7FBE615554026DFFEAD1431696F48E241C (Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE* __this, RuntimeObject* ___0_board, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Leaderboard_tBDB34CC6F79318BE6D7761015C70C8A5CC64EC71_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	bool V_1 = false;
	{
		bool L_0;
		L_0 = Local_VerifyUser_m8AAA9F80FAFDA28FF4026A1532ABD34A4CDD88A1(__this, NULL);
		V_0 = (bool)((((int32_t)L_0) == ((int32_t)0))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_0012;
		}
	}
	{
		V_1 = (bool)0;
		goto IL_0020;
	}

IL_0012:
	{
		RuntimeObject* L_2 = ___0_board;
		NullCheck(((Leaderboard_tBDB34CC6F79318BE6D7761015C70C8A5CC64EC71*)CastclassClass((RuntimeObject*)L_2, Leaderboard_tBDB34CC6F79318BE6D7761015C70C8A5CC64EC71_il2cpp_TypeInfo_var)));
		bool L_3;
		L_3 = Leaderboard_get_loading_mE6CC3F9AE66A0909F02F3EE8BBCD8DA9B4BB8A39(((Leaderboard_tBDB34CC6F79318BE6D7761015C70C8A5CC64EC71*)CastclassClass((RuntimeObject*)L_2, Leaderboard_tBDB34CC6F79318BE6D7761015C70C8A5CC64EC71_il2cpp_TypeInfo_var)), NULL);
		V_1 = L_3;
		goto IL_0020;
	}

IL_0020:
	{
		bool L_4 = V_1;
		return L_4;
	}
}
// System.Boolean UnityEngine.SocialPlatforms.Local::VerifyUser()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Local_VerifyUser_m8AAA9F80FAFDA28FF4026A1532ABD34A4CDD88A1 (Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ILocalUser_t8D7F34634AF940D34302E4F415AD50C7132EB7F6_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral697DAE71CF742F2932950075B1D260B4A087E7BA);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	bool V_1 = false;
	{
		RuntimeObject* L_0;
		L_0 = Local_get_localUser_m21888B1B23DBA198D651B239D51DBCA525474F9E(__this, NULL);
		NullCheck(L_0);
		bool L_1;
		L_1 = InterfaceFuncInvoker0< bool >::Invoke(0 /* System.Boolean UnityEngine.SocialPlatforms.ILocalUser::get_authenticated() */, ILocalUser_t8D7F34634AF940D34302E4F415AD50C7132EB7F6_il2cpp_TypeInfo_var, L_0);
		V_0 = (bool)((((int32_t)L_1) == ((int32_t)0))? 1 : 0);
		bool L_2 = V_0;
		if (!L_2)
		{
			goto IL_0023;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(_stringLiteral697DAE71CF742F2932950075B1D260B4A087E7BA, NULL);
		V_1 = (bool)0;
		goto IL_0027;
	}

IL_0023:
	{
		V_1 = (bool)1;
		goto IL_0027;
	}

IL_0027:
	{
		bool L_3 = V_1;
		return L_3;
	}
}
// System.Void UnityEngine.SocialPlatforms.Local::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Local__ctor_m223523E0079C7C184D1B804092E5C0A61B0E110E (Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_m00D08220BB0FBFE450F57049A4EEFA0898BDF022_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_m23072805C6943D45A688FD6F11097CA8AFC14084_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_m42603AF3F773C24EEBCCAF48DB31A596CCFBFFB3_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_m59416295F911DEAE5FD7681CF4CD1FF1221370BE_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_t3174CD866C458170B5DD174A25DD3D19A5C3B3B7_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_t5B1BB048CE74B294094EE23B5B5546279A93E60A_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_tA7AA760D42FE7082707AC34B0D3327F25696E8D9_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_tE6E66411E6B6A22232C67EAAEF4901A3486E55B3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		List_1_tE6E66411E6B6A22232C67EAAEF4901A3486E55B3* L_0 = (List_1_tE6E66411E6B6A22232C67EAAEF4901A3486E55B3*)il2cpp_codegen_object_new(List_1_tE6E66411E6B6A22232C67EAAEF4901A3486E55B3_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		List_1__ctor_m59416295F911DEAE5FD7681CF4CD1FF1221370BE(L_0, List_1__ctor_m59416295F911DEAE5FD7681CF4CD1FF1221370BE_RuntimeMethod_var);
		__this->___m_Friends_1 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_Friends_1), (void*)L_0);
		List_1_tE6E66411E6B6A22232C67EAAEF4901A3486E55B3* L_1 = (List_1_tE6E66411E6B6A22232C67EAAEF4901A3486E55B3*)il2cpp_codegen_object_new(List_1_tE6E66411E6B6A22232C67EAAEF4901A3486E55B3_il2cpp_TypeInfo_var);
		NullCheck(L_1);
		List_1__ctor_m59416295F911DEAE5FD7681CF4CD1FF1221370BE(L_1, List_1__ctor_m59416295F911DEAE5FD7681CF4CD1FF1221370BE_RuntimeMethod_var);
		__this->___m_Users_2 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_Users_2), (void*)L_1);
		List_1_t3174CD866C458170B5DD174A25DD3D19A5C3B3B7* L_2 = (List_1_t3174CD866C458170B5DD174A25DD3D19A5C3B3B7*)il2cpp_codegen_object_new(List_1_t3174CD866C458170B5DD174A25DD3D19A5C3B3B7_il2cpp_TypeInfo_var);
		NullCheck(L_2);
		List_1__ctor_m00D08220BB0FBFE450F57049A4EEFA0898BDF022(L_2, List_1__ctor_m00D08220BB0FBFE450F57049A4EEFA0898BDF022_RuntimeMethod_var);
		__this->___m_AchievementDescriptions_3 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_AchievementDescriptions_3), (void*)L_2);
		List_1_t5B1BB048CE74B294094EE23B5B5546279A93E60A* L_3 = (List_1_t5B1BB048CE74B294094EE23B5B5546279A93E60A*)il2cpp_codegen_object_new(List_1_t5B1BB048CE74B294094EE23B5B5546279A93E60A_il2cpp_TypeInfo_var);
		NullCheck(L_3);
		List_1__ctor_m42603AF3F773C24EEBCCAF48DB31A596CCFBFFB3(L_3, List_1__ctor_m42603AF3F773C24EEBCCAF48DB31A596CCFBFFB3_RuntimeMethod_var);
		__this->___m_Achievements_4 = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_Achievements_4), (void*)L_3);
		List_1_tA7AA760D42FE7082707AC34B0D3327F25696E8D9* L_4 = (List_1_tA7AA760D42FE7082707AC34B0D3327F25696E8D9*)il2cpp_codegen_object_new(List_1_tA7AA760D42FE7082707AC34B0D3327F25696E8D9_il2cpp_TypeInfo_var);
		NullCheck(L_4);
		List_1__ctor_m23072805C6943D45A688FD6F11097CA8AFC14084(L_4, List_1__ctor_m23072805C6943D45A688FD6F11097CA8AFC14084_RuntimeMethod_var);
		__this->___m_Leaderboards_5 = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_Leaderboards_5), (void*)L_4);
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
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
// UnityEngine.SocialPlatforms.ISocialPlatform UnityEngine.SocialPlatforms.ActivePlatform::get_Instance()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ActivePlatform_get_Instance_mC9DF8265897D79151D3F86D48A73691B6E3AFA06 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ActivePlatform_tA561A1F6671B5E621A6A1BB446EF480126BE2624_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	RuntimeObject* V_1 = NULL;
	{
		RuntimeObject* L_0 = ((ActivePlatform_tA561A1F6671B5E621A6A1BB446EF480126BE2624_StaticFields*)il2cpp_codegen_static_fields_for(ActivePlatform_tA561A1F6671B5E621A6A1BB446EF480126BE2624_il2cpp_TypeInfo_var))->____active_0;
		V_0 = (bool)((((RuntimeObject*)(RuntimeObject*)L_0) == ((RuntimeObject*)(RuntimeObject*)NULL))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_0017;
		}
	}
	{
		RuntimeObject* L_2;
		L_2 = ActivePlatform_SelectSocialPlatform_mEA0704F85B0FD046DE2E4069B1EC413BD835BCA7(NULL);
		((ActivePlatform_tA561A1F6671B5E621A6A1BB446EF480126BE2624_StaticFields*)il2cpp_codegen_static_fields_for(ActivePlatform_tA561A1F6671B5E621A6A1BB446EF480126BE2624_il2cpp_TypeInfo_var))->____active_0 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&((ActivePlatform_tA561A1F6671B5E621A6A1BB446EF480126BE2624_StaticFields*)il2cpp_codegen_static_fields_for(ActivePlatform_tA561A1F6671B5E621A6A1BB446EF480126BE2624_il2cpp_TypeInfo_var))->____active_0), (void*)L_2);
	}

IL_0017:
	{
		RuntimeObject* L_3 = ((ActivePlatform_tA561A1F6671B5E621A6A1BB446EF480126BE2624_StaticFields*)il2cpp_codegen_static_fields_for(ActivePlatform_tA561A1F6671B5E621A6A1BB446EF480126BE2624_il2cpp_TypeInfo_var))->____active_0;
		V_1 = L_3;
		goto IL_001f;
	}

IL_001f:
	{
		RuntimeObject* L_4 = V_1;
		return L_4;
	}
}
// System.Void UnityEngine.SocialPlatforms.ActivePlatform::set_Instance(UnityEngine.SocialPlatforms.ISocialPlatform)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ActivePlatform_set_Instance_m11EC15E4BB9909B697B814DAA5C5C8314DE702E9 (RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ActivePlatform_tA561A1F6671B5E621A6A1BB446EF480126BE2624_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		RuntimeObject* L_0 = ___0_value;
		((ActivePlatform_tA561A1F6671B5E621A6A1BB446EF480126BE2624_StaticFields*)il2cpp_codegen_static_fields_for(ActivePlatform_tA561A1F6671B5E621A6A1BB446EF480126BE2624_il2cpp_TypeInfo_var))->____active_0 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((ActivePlatform_tA561A1F6671B5E621A6A1BB446EF480126BE2624_StaticFields*)il2cpp_codegen_static_fields_for(ActivePlatform_tA561A1F6671B5E621A6A1BB446EF480126BE2624_il2cpp_TypeInfo_var))->____active_0), (void*)L_0);
		return;
	}
}
// UnityEngine.SocialPlatforms.ISocialPlatform UnityEngine.SocialPlatforms.ActivePlatform::SelectSocialPlatform()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ActivePlatform_SelectSocialPlatform_mEA0704F85B0FD046DE2E4069B1EC413BD835BCA7 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	{
		Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE* L_0 = (Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE*)il2cpp_codegen_object_new(Local_t9043C59C2E8F6360DA4263524B306CB5B19A66FE_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		Local__ctor_m223523E0079C7C184D1B804092E5C0A61B0E110E(L_0, NULL);
		V_0 = L_0;
		goto IL_0009;
	}

IL_0009:
	{
		RuntimeObject* L_1 = V_0;
		return L_1;
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
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void UnityEngine.SocialPlatforms.Impl.LocalUser::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LocalUser__ctor_m6D2AE6DFC61CEC39842944D970E2B2B5547CBE97 (LocalUser_t55C68E98993F86B6FBB7A25F28EB989CD7E6A3AD* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UserProfileU5BU5D_tCE4194A0D6665FFF7943DDC3B0B9301D57F84A6A_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	IUserProfileU5BU5D_t0179D2FF9BD9F78A4E0A10AE350DC1F19E5FCB43* V_0 = NULL;
	{
		UserProfile__ctor_m12A26F8BDE41F4B55A645BB1D4038E81A877E680(__this, NULL);
		UserProfileU5BU5D_tCE4194A0D6665FFF7943DDC3B0B9301D57F84A6A* L_0 = (UserProfileU5BU5D_tCE4194A0D6665FFF7943DDC3B0B9301D57F84A6A*)(UserProfileU5BU5D_tCE4194A0D6665FFF7943DDC3B0B9301D57F84A6A*)SZArrayNew(UserProfileU5BU5D_tCE4194A0D6665FFF7943DDC3B0B9301D57F84A6A_il2cpp_TypeInfo_var, (uint32_t)0);
		V_0 = (IUserProfileU5BU5D_t0179D2FF9BD9F78A4E0A10AE350DC1F19E5FCB43*)L_0;
		IUserProfileU5BU5D_t0179D2FF9BD9F78A4E0A10AE350DC1F19E5FCB43* L_1 = V_0;
		__this->___m_Friends_6 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_Friends_6), (void*)L_1);
		__this->___m_Authenticated_7 = (bool)0;
		__this->___m_Underage_8 = (bool)0;
		return;
	}
}
// System.Boolean UnityEngine.SocialPlatforms.Impl.LocalUser::get_authenticated()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool LocalUser_get_authenticated_m3121DA81FF48CFFB4024ADECEE98F8E686497C54 (LocalUser_t55C68E98993F86B6FBB7A25F28EB989CD7E6A3AD* __this, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		bool L_0 = __this->___m_Authenticated_7;
		V_0 = L_0;
		goto IL_000a;
	}

IL_000a:
	{
		bool L_1 = V_0;
		return L_1;
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
// System.Void UnityEngine.SocialPlatforms.Impl.UserProfile::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UserProfile__ctor_m12A26F8BDE41F4B55A645BB1D4038E81A877E680 (UserProfile_t3EF35349E23201EF9F3C5956C44384FA45C1EF29* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5ECA19F316C4BF30DC6AFDC7822F68EDF20BDA1E);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralF944DCD635F9801F7AC90A407FBC479964DEC024);
		s_Il2CppMethodInitialized = true;
	}
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		__this->___m_UserName_0 = _stringLiteral5ECA19F316C4BF30DC6AFDC7822F68EDF20BDA1E;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_UserName_0), (void*)_stringLiteral5ECA19F316C4BF30DC6AFDC7822F68EDF20BDA1E);
		__this->___m_ID_1 = _stringLiteralF944DCD635F9801F7AC90A407FBC479964DEC024;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_ID_1), (void*)_stringLiteralF944DCD635F9801F7AC90A407FBC479964DEC024);
		__this->___m_legacyID_2 = _stringLiteralF944DCD635F9801F7AC90A407FBC479964DEC024;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_legacyID_2), (void*)_stringLiteralF944DCD635F9801F7AC90A407FBC479964DEC024);
		__this->___m_IsFriend_3 = (bool)0;
		__this->___m_State_4 = 3;
		Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* L_0 = (Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4*)il2cpp_codegen_object_new(Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		Texture2D__ctor_m3BA82E87442B7F69E118477069AE11101B9DF796(L_0, ((int32_t)32), ((int32_t)32), NULL);
		__this->___m_Image_5 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_Image_5), (void*)L_0);
		return;
	}
}
// System.String UnityEngine.SocialPlatforms.Impl.UserProfile::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* UserProfile_ToString_mEB091241EC114F4F42D2CE15F127B83556EBE45D (UserProfile_t3EF35349E23201EF9F3C5956C44384FA45C1EF29* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UserState_t4C54A6F5CE00515F37F91DFB88AEB8FC56C6934C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral960E5E7F211EFF3243DF14EDD1901DC9EF314D62);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	int32_t V_1 = 0;
	String_t* V_2 = NULL;
	{
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_0 = (StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)(StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)SZArrayNew(StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var, (uint32_t)7);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_1 = L_0;
		String_t* L_2;
		L_2 = UserProfile_get_id_m16A4060A0C7E4480F68D6915E6FAB15EAE973336(__this, NULL);
		NullCheck(L_1);
		ArrayElementTypeCheck (L_1, L_2);
		(L_1)->SetAt(static_cast<il2cpp_array_size_t>(0), (String_t*)L_2);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_3 = L_1;
		NullCheck(L_3);
		ArrayElementTypeCheck (L_3, _stringLiteral960E5E7F211EFF3243DF14EDD1901DC9EF314D62);
		(L_3)->SetAt(static_cast<il2cpp_array_size_t>(1), (String_t*)_stringLiteral960E5E7F211EFF3243DF14EDD1901DC9EF314D62);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_4 = L_3;
		String_t* L_5;
		L_5 = UserProfile_get_userName_mDAAF12B06B939DDAAB6F10E8CB40B21C48A94F30(__this, NULL);
		NullCheck(L_4);
		ArrayElementTypeCheck (L_4, L_5);
		(L_4)->SetAt(static_cast<il2cpp_array_size_t>(2), (String_t*)L_5);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_6 = L_4;
		NullCheck(L_6);
		ArrayElementTypeCheck (L_6, _stringLiteral960E5E7F211EFF3243DF14EDD1901DC9EF314D62);
		(L_6)->SetAt(static_cast<il2cpp_array_size_t>(3), (String_t*)_stringLiteral960E5E7F211EFF3243DF14EDD1901DC9EF314D62);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_7 = L_6;
		bool L_8;
		L_8 = UserProfile_get_isFriend_m353D113C22BFA80F0E9A1DBEC40E4EF4984AC4EC(__this, NULL);
		V_0 = L_8;
		il2cpp_codegen_runtime_class_init_inline(Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		String_t* L_9;
		L_9 = Boolean_ToString_m6646C8026B1DF381A1EE8CD13549175E9703CC63((&V_0), NULL);
		NullCheck(L_7);
		ArrayElementTypeCheck (L_7, L_9);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(4), (String_t*)L_9);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_10 = L_7;
		NullCheck(L_10);
		ArrayElementTypeCheck (L_10, _stringLiteral960E5E7F211EFF3243DF14EDD1901DC9EF314D62);
		(L_10)->SetAt(static_cast<il2cpp_array_size_t>(5), (String_t*)_stringLiteral960E5E7F211EFF3243DF14EDD1901DC9EF314D62);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_11 = L_10;
		int32_t L_12;
		L_12 = UserProfile_get_state_mF5F8CF4E71CD46ADBCC58E5A3AFA715B3E5F9D4A(__this, NULL);
		V_1 = L_12;
		Il2CppFakeBox<int32_t> L_13(UserState_t4C54A6F5CE00515F37F91DFB88AEB8FC56C6934C_il2cpp_TypeInfo_var, (&V_1));
		String_t* L_14;
		L_14 = Enum_ToString_m946B0B83C4470457D0FF555D862022C72BB55741((Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2*)(&L_13), NULL);
		NullCheck(L_11);
		ArrayElementTypeCheck (L_11, L_14);
		(L_11)->SetAt(static_cast<il2cpp_array_size_t>(6), (String_t*)L_14);
		String_t* L_15;
		L_15 = String_Concat_m647EBF831F54B6DF7D5AFA5FD012CF4EE7571B6A(L_11, NULL);
		V_2 = L_15;
		goto IL_0061;
	}

IL_0061:
	{
		String_t* L_16 = V_2;
		return L_16;
	}
}
// System.String UnityEngine.SocialPlatforms.Impl.UserProfile::get_userName()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* UserProfile_get_userName_mDAAF12B06B939DDAAB6F10E8CB40B21C48A94F30 (UserProfile_t3EF35349E23201EF9F3C5956C44384FA45C1EF29* __this, const RuntimeMethod* method) 
{
	String_t* V_0 = NULL;
	{
		String_t* L_0 = __this->___m_UserName_0;
		V_0 = L_0;
		goto IL_000a;
	}

IL_000a:
	{
		String_t* L_1 = V_0;
		return L_1;
	}
}
// System.String UnityEngine.SocialPlatforms.Impl.UserProfile::get_id()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* UserProfile_get_id_m16A4060A0C7E4480F68D6915E6FAB15EAE973336 (UserProfile_t3EF35349E23201EF9F3C5956C44384FA45C1EF29* __this, const RuntimeMethod* method) 
{
	String_t* V_0 = NULL;
	{
		String_t* L_0 = __this->___m_ID_1;
		V_0 = L_0;
		goto IL_000a;
	}

IL_000a:
	{
		String_t* L_1 = V_0;
		return L_1;
	}
}
// System.Boolean UnityEngine.SocialPlatforms.Impl.UserProfile::get_isFriend()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool UserProfile_get_isFriend_m353D113C22BFA80F0E9A1DBEC40E4EF4984AC4EC (UserProfile_t3EF35349E23201EF9F3C5956C44384FA45C1EF29* __this, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		bool L_0 = __this->___m_IsFriend_3;
		V_0 = L_0;
		goto IL_000a;
	}

IL_000a:
	{
		bool L_1 = V_0;
		return L_1;
	}
}
// UnityEngine.SocialPlatforms.UserState UnityEngine.SocialPlatforms.Impl.UserProfile::get_state()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t UserProfile_get_state_mF5F8CF4E71CD46ADBCC58E5A3AFA715B3E5F9D4A (UserProfile_t3EF35349E23201EF9F3C5956C44384FA45C1EF29* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	{
		int32_t L_0 = __this->___m_State_4;
		V_0 = L_0;
		goto IL_000a;
	}

IL_000a:
	{
		int32_t L_1 = V_0;
		return L_1;
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
// System.Void UnityEngine.SocialPlatforms.Impl.Achievement::.ctor(System.String,System.Double,System.Boolean,System.Boolean,System.DateTime)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Achievement__ctor_mE4B26ACD8E4220F645864036B7F9AD2E7D746B85 (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, String_t* ___0_id, double ___1_percentCompleted, bool ___2_completed, bool ___3_hidden, DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___4_lastReportedDate, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		String_t* L_0 = ___0_id;
		Achievement_set_id_mF2FB90F3D2F1F38DBDB449C956B8B35BAF6108EE_inline(__this, L_0, NULL);
		double L_1 = ___1_percentCompleted;
		Achievement_set_percentCompleted_mFB33E191E8D7557E178EEC6DB80B616C268824BD_inline(__this, L_1, NULL);
		bool L_2 = ___2_completed;
		__this->___m_Completed_0 = L_2;
		bool L_3 = ___3_hidden;
		__this->___m_Hidden_1 = L_3;
		DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D L_4 = ___4_lastReportedDate;
		__this->___m_LastReportedDate_2 = L_4;
		return;
	}
}
// System.String UnityEngine.SocialPlatforms.Impl.Achievement::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Achievement_ToString_m15B103F95C549FCED0D581FBACD73E47310C9EAC (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral960E5E7F211EFF3243DF14EDD1901DC9EF314D62);
		s_Il2CppMethodInitialized = true;
	}
	double V_0 = 0.0;
	bool V_1 = false;
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D V_2;
	memset((&V_2), 0, sizeof(V_2));
	String_t* V_3 = NULL;
	{
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_0 = (StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)(StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)SZArrayNew(StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var, (uint32_t)((int32_t)9));
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_1 = L_0;
		String_t* L_2;
		L_2 = Achievement_get_id_m0BDCED7ECE8BA170E619212CB2D23BD28322A0A6_inline(__this, NULL);
		NullCheck(L_1);
		ArrayElementTypeCheck (L_1, L_2);
		(L_1)->SetAt(static_cast<il2cpp_array_size_t>(0), (String_t*)L_2);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_3 = L_1;
		NullCheck(L_3);
		ArrayElementTypeCheck (L_3, _stringLiteral960E5E7F211EFF3243DF14EDD1901DC9EF314D62);
		(L_3)->SetAt(static_cast<il2cpp_array_size_t>(1), (String_t*)_stringLiteral960E5E7F211EFF3243DF14EDD1901DC9EF314D62);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_4 = L_3;
		double L_5;
		L_5 = Achievement_get_percentCompleted_mC0BFB768F39C110F2A851B5089C300E1C879D860_inline(__this, NULL);
		V_0 = L_5;
		String_t* L_6;
		L_6 = Double_ToString_m7499A5D792419537DCB9470A3675CEF5117DE339((&V_0), NULL);
		NullCheck(L_4);
		ArrayElementTypeCheck (L_4, L_6);
		(L_4)->SetAt(static_cast<il2cpp_array_size_t>(2), (String_t*)L_6);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_7 = L_4;
		NullCheck(L_7);
		ArrayElementTypeCheck (L_7, _stringLiteral960E5E7F211EFF3243DF14EDD1901DC9EF314D62);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(3), (String_t*)_stringLiteral960E5E7F211EFF3243DF14EDD1901DC9EF314D62);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_8 = L_7;
		bool L_9;
		L_9 = Achievement_get_completed_m5A625BD69E129E468D83A98CF4CAA4AA28E0B2BF(__this, NULL);
		V_1 = L_9;
		il2cpp_codegen_runtime_class_init_inline(Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		String_t* L_10;
		L_10 = Boolean_ToString_m6646C8026B1DF381A1EE8CD13549175E9703CC63((&V_1), NULL);
		NullCheck(L_8);
		ArrayElementTypeCheck (L_8, L_10);
		(L_8)->SetAt(static_cast<il2cpp_array_size_t>(4), (String_t*)L_10);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_11 = L_8;
		NullCheck(L_11);
		ArrayElementTypeCheck (L_11, _stringLiteral960E5E7F211EFF3243DF14EDD1901DC9EF314D62);
		(L_11)->SetAt(static_cast<il2cpp_array_size_t>(5), (String_t*)_stringLiteral960E5E7F211EFF3243DF14EDD1901DC9EF314D62);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_12 = L_11;
		bool L_13;
		L_13 = Achievement_get_hidden_m343030FB2BE6636313A09DEDCB02BE0ADB5E3A8E(__this, NULL);
		V_1 = L_13;
		String_t* L_14;
		L_14 = Boolean_ToString_m6646C8026B1DF381A1EE8CD13549175E9703CC63((&V_1), NULL);
		NullCheck(L_12);
		ArrayElementTypeCheck (L_12, L_14);
		(L_12)->SetAt(static_cast<il2cpp_array_size_t>(6), (String_t*)L_14);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_15 = L_12;
		NullCheck(L_15);
		ArrayElementTypeCheck (L_15, _stringLiteral960E5E7F211EFF3243DF14EDD1901DC9EF314D62);
		(L_15)->SetAt(static_cast<il2cpp_array_size_t>(7), (String_t*)_stringLiteral960E5E7F211EFF3243DF14EDD1901DC9EF314D62);
		StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_16 = L_15;
		DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D L_17;
		L_17 = Achievement_get_lastReportedDate_mEACB3BADB0996AF967B26378FA48BC964E68F091(__this, NULL);
		V_2 = L_17;
		il2cpp_codegen_runtime_class_init_inline(DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D_il2cpp_TypeInfo_var);
		String_t* L_18;
		L_18 = DateTime_ToString_m447C83E1F8FFFFF4D20C0F7D5C18DEB160F9833A((&V_2), NULL);
		NullCheck(L_16);
		ArrayElementTypeCheck (L_16, L_18);
		(L_16)->SetAt(static_cast<il2cpp_array_size_t>(8), (String_t*)L_18);
		String_t* L_19;
		L_19 = String_Concat_m647EBF831F54B6DF7D5AFA5FD012CF4EE7571B6A(L_16, NULL);
		V_3 = L_19;
		goto IL_007d;
	}

IL_007d:
	{
		String_t* L_20 = V_3;
		return L_20;
	}
}
// System.Void UnityEngine.SocialPlatforms.Impl.Achievement::SetCompleted(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Achievement_SetCompleted_mE342B402331E79D656CBC5E7FE881F69641D1E88 (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, bool ___0_value, const RuntimeMethod* method) 
{
	{
		bool L_0 = ___0_value;
		__this->___m_Completed_0 = L_0;
		return;
	}
}
// System.Void UnityEngine.SocialPlatforms.Impl.Achievement::SetHidden(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Achievement_SetHidden_m9D0CA6D3F8539818DB88E3174AB57D4746B9CDCE (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, bool ___0_value, const RuntimeMethod* method) 
{
	{
		bool L_0 = ___0_value;
		__this->___m_Hidden_1 = L_0;
		return;
	}
}
// System.Void UnityEngine.SocialPlatforms.Impl.Achievement::SetLastReportedDate(System.DateTime)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Achievement_SetLastReportedDate_m0235AF958908983C7B025090C5A4061D27F7B3F3 (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D ___0_date, const RuntimeMethod* method) 
{
	{
		DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D L_0 = ___0_date;
		__this->___m_LastReportedDate_2 = L_0;
		return;
	}
}
// System.String UnityEngine.SocialPlatforms.Impl.Achievement::get_id()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Achievement_get_id_m0BDCED7ECE8BA170E619212CB2D23BD28322A0A6 (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = __this->___U3CidU3Ek__BackingField_3;
		return L_0;
	}
}
// System.Void UnityEngine.SocialPlatforms.Impl.Achievement::set_id(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Achievement_set_id_mF2FB90F3D2F1F38DBDB449C956B8B35BAF6108EE (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, String_t* ___0_value, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = ___0_value;
		__this->___U3CidU3Ek__BackingField_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CidU3Ek__BackingField_3), (void*)L_0);
		return;
	}
}
// System.Double UnityEngine.SocialPlatforms.Impl.Achievement::get_percentCompleted()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double Achievement_get_percentCompleted_mC0BFB768F39C110F2A851B5089C300E1C879D860 (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, const RuntimeMethod* method) 
{
	{
		double L_0 = __this->___U3CpercentCompletedU3Ek__BackingField_4;
		return L_0;
	}
}
// System.Void UnityEngine.SocialPlatforms.Impl.Achievement::set_percentCompleted(System.Double)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Achievement_set_percentCompleted_mFB33E191E8D7557E178EEC6DB80B616C268824BD (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, double ___0_value, const RuntimeMethod* method) 
{
	{
		double L_0 = ___0_value;
		__this->___U3CpercentCompletedU3Ek__BackingField_4 = L_0;
		return;
	}
}
// System.Boolean UnityEngine.SocialPlatforms.Impl.Achievement::get_completed()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Achievement_get_completed_m5A625BD69E129E468D83A98CF4CAA4AA28E0B2BF (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		bool L_0 = __this->___m_Completed_0;
		V_0 = L_0;
		goto IL_000a;
	}

IL_000a:
	{
		bool L_1 = V_0;
		return L_1;
	}
}
// System.Boolean UnityEngine.SocialPlatforms.Impl.Achievement::get_hidden()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Achievement_get_hidden_m343030FB2BE6636313A09DEDCB02BE0ADB5E3A8E (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, const RuntimeMethod* method) 
{
	bool V_0 = false;
	{
		bool L_0 = __this->___m_Hidden_1;
		V_0 = L_0;
		goto IL_000a;
	}

IL_000a:
	{
		bool L_1 = V_0;
		return L_1;
	}
}
// System.DateTime UnityEngine.SocialPlatforms.Impl.Achievement::get_lastReportedDate()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D Achievement_get_lastReportedDate_mEACB3BADB0996AF967B26378FA48BC964E68F091 (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, const RuntimeMethod* method) 
{
	DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D L_0 = __this->___m_LastReportedDate_2;
		V_0 = L_0;
		goto IL_000a;
	}

IL_000a:
	{
		DateTime_t66193957C73913903DDAD89FEDC46139BCA5802D L_1 = V_0;
		return L_1;
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
// System.String UnityEngine.SocialPlatforms.Impl.AchievementDescription::get_id()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AchievementDescription_get_id_mC954988F8344E3B7E316D15EBD240A85D06A77C7 (AchievementDescription_t0D2306DF6EE55C872DB06E6855D7B1AE0E6DDEF9* __this, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = __this->___U3CidU3Ek__BackingField_0;
		return L_0;
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
// System.Boolean UnityEngine.SocialPlatforms.Impl.Leaderboard::get_loading()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Leaderboard_get_loading_mE6CC3F9AE66A0909F02F3EE8BBCD8DA9B4BB8A39 (Leaderboard_tBDB34CC6F79318BE6D7761015C70C8A5CC64EC71* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ISocialPlatform_tA236686987B4CB8A0694EEBAB4D7EB57CBABA254_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	{
		RuntimeObject* L_0;
		L_0 = ActivePlatform_get_Instance_mC9DF8265897D79151D3F86D48A73691B6E3AFA06(NULL);
		NullCheck(L_0);
		bool L_1;
		L_1 = InterfaceFuncInvoker1< bool, RuntimeObject* >::Invoke(1 /* System.Boolean UnityEngine.SocialPlatforms.ISocialPlatform::GetLoading(UnityEngine.SocialPlatforms.ILeaderboard) */, ISocialPlatform_tA236686987B4CB8A0694EEBAB4D7EB57CBABA254_il2cpp_TypeInfo_var, L_0, __this);
		V_0 = L_1;
		goto IL_000f;
	}

IL_000f:
	{
		bool L_2 = V_0;
		return L_2;
	}
}
// System.String UnityEngine.SocialPlatforms.Impl.Leaderboard::get_id()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Leaderboard_get_id_mA5577E5D07BEE409EF2CDE6177AEB90547554770 (Leaderboard_tBDB34CC6F79318BE6D7761015C70C8A5CC64EC71* __this, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = __this->___U3CidU3Ek__BackingField_0;
		return L_0;
	}
}
// UnityEngine.SocialPlatforms.UserScope UnityEngine.SocialPlatforms.Impl.Leaderboard::get_userScope()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Leaderboard_get_userScope_m84E19D835910E26104D25567BA0B1C6A518FC5C1 (Leaderboard_tBDB34CC6F79318BE6D7761015C70C8A5CC64EC71* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___U3CuserScopeU3Ek__BackingField_1;
		return L_0;
	}
}
// UnityEngine.SocialPlatforms.Range UnityEngine.SocialPlatforms.Impl.Leaderboard::get_range()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Range_tDDBAD7CBDC5DD273DA4330F4E9CDF24C62F16ED6 Leaderboard_get_range_m98008ADA839E76C8D69D152F7FC6EDBF2F6985DB (Leaderboard_tBDB34CC6F79318BE6D7761015C70C8A5CC64EC71* __this, const RuntimeMethod* method) 
{
	{
		Range_tDDBAD7CBDC5DD273DA4330F4E9CDF24C62F16ED6 L_0 = __this->___U3CrangeU3Ek__BackingField_2;
		return L_0;
	}
}
// UnityEngine.SocialPlatforms.TimeScope UnityEngine.SocialPlatforms.Impl.Leaderboard::get_timeScope()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Leaderboard_get_timeScope_mB7893524B11F9CF2C739B269C6CA34F19B1FC95E (Leaderboard_tBDB34CC6F79318BE6D7761015C70C8A5CC64EC71* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___U3CtimeScopeU3Ek__BackingField_3;
		return L_0;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* Achievement_get_id_m0BDCED7ECE8BA170E619212CB2D23BD28322A0A6_inline (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = __this->___U3CidU3Ek__BackingField_3;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR double Achievement_get_percentCompleted_mC0BFB768F39C110F2A851B5089C300E1C879D860_inline (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, const RuntimeMethod* method) 
{
	{
		double L_0 = __this->___U3CpercentCompletedU3Ek__BackingField_4;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Achievement_set_percentCompleted_mFB33E191E8D7557E178EEC6DB80B616C268824BD_inline (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, double ___0_value, const RuntimeMethod* method) 
{
	{
		double L_0 = ___0_value;
		__this->___U3CpercentCompletedU3Ek__BackingField_4 = L_0;
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* AchievementDescription_get_id_mC954988F8344E3B7E316D15EBD240A85D06A77C7_inline (AchievementDescription_t0D2306DF6EE55C872DB06E6855D7B1AE0E6DDEF9* __this, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = __this->___U3CidU3Ek__BackingField_0;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Achievement_set_id_mF2FB90F3D2F1F38DBDB449C956B8B35BAF6108EE_inline (Achievement_t723EE724DCEBFAE9555CDC909FDA84F71EB5719B* __this, String_t* ___0_value, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = ___0_value;
		__this->___U3CidU3Ek__BackingField_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CidU3Ek__BackingField_3), (void*)L_0);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_gshared_inline (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = __this->____current_3;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_m69C8773D6967F3B224777183E24EA621CE056F8F_gshared_inline (Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* __this, bool ___0_obj, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, bool, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version_3;
		__this->____version_3 = ((int32_t)il2cpp_codegen_add(L_0, 1));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = __this->____items_1;
		V_0 = L_1;
		int32_t L_2 = __this->____size_2;
		V_1 = L_2;
		int32_t L_3 = V_1;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_4 = V_0;
		NullCheck(L_4);
		if ((!(((uint32_t)L_3) < ((uint32_t)((int32_t)(((RuntimeArray*)L_4)->max_length))))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_5 = V_1;
		__this->____size_2 = ((int32_t)il2cpp_codegen_add(L_5, 1));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = V_0;
		int32_t L_7 = V_1;
		RuntimeObject* L_8 = ___0_item;
		NullCheck(L_6);
		(L_6)->SetAt(static_cast<il2cpp_array_size_t>(L_7), (RuntimeObject*)L_8);
		return;
	}

IL_0034:
	{
		RuntimeObject* L_9 = ___0_item;
		((  void (*) (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D*, RuntimeObject*, const RuntimeMethod*))il2cpp_codegen_get_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 11)))(__this, L_9, il2cpp_rgctx_method(method->klass->rgctx_data, 11));
		return;
	}
}
