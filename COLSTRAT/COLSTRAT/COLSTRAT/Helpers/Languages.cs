using System;
using System.Collections.Generic;
namespace COLSTRAT.Helpers
{
    using Xamarin.Forms;
    using Interfaces;
    using Resources;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resources.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }
        public static string Delete
        {
            get
            {
                return Resources.delete;
            }
        }
        public static string Edit
        {
            get
            {
                return Resources.edit;
            }
        }
        public static string Label_Sync_Info
        {
            get
            {
                return Resources.label_sync_info;
            }
        }
        public static string Message_Not_Data
        {
            get
            {
                return Resources.message_not_data;
            }
        }
        public static string Sync_Success
        {
            get
            {
                return Resources.sync_success;
            }
        }
        
        public static string Sync_Not_Data
        {
            get
            {
                return Resources.message_not_data_forsync;
            }
        }
        public static string Sync_Data
        {
            get
            {
                return Resources.sync_title;
            }
        }
        public static string User_Error_Info
        {
            get
            {
                return Resources.error_user_information;
            }
        }
        
        public static string Message_Save_OnLocal
        {
            get
            {
                return Resources.message_save_onlocaldb;
            }
        }
        public static string Message_Title
        {
            get
            {
                return Resources.message_title;
            }
        }
        public static string Confirm_Exit
        {
            get
            {
                return Resources.confirm_exit;
            }
        }
        public static string Email_Invalid
        {
            get
            {
                return Resources.error_email_invalid;
            }
        }
        public static string Login_With_FB
        {
            get
            {
                return Resources.LoginWithFacebook;
            }
        }
        public static string First_Name_Error
        {
            get
            {
                return Resources.error_input_first_name;
            }
        }
        public static string Last_Name_Error
        {
            get
            {
                return Resources.error_input_last_name;
            }
        }
        public static string Pass_Error_Length
        {
            get
            {
                return Resources.error_pass_length;
            }
        }
        public static string Pass_Confirm_Error_Input
        {
            get
            {
                return Resources.error_input_pass_confirm;
            }
        }
        public static string Pass_Confirm_NotMatch
        {
            get
            {
                return Resources.pass_confirm_notmatch;
            }
        }
        public static string Email_Exists
        {
            get
            {
                return Resources.email_exists;
            }
        }
        public static string Profile
        {
            get
            {
                return Resources.menu_profile;
            }
        }
        public static string Ubication
        {
            get
            {
                return Resources.menu_ubication;
            }
        }
        public static string Home
        {
            get
            {
                return Resources.home;
            }
        }
        public static string Option_Pick_Photo
        {
            get
            {
                return Resources.option_select_image;
            }
        }
        public static string From_Gallery
        {
            get
            {
                return Resources.from_gallery;
            }
        }
        public static string From_Camera
        {
            get
            {
                return Resources.from_camera;
            }
        }
        public static string Cancel
        {
            get
            {
                return Resources.cancel;
            }
        }
        public static string Error_Record_Relateds
        {
            get
            {
                return Resources.error_relateds;
            }
        }
        public static string Message_Delete
        {
            get
            {
                return Resources.message_delete;
            }
        }
        public static string Error_Record_Same
        {
            get { return Resources.error_record_same; }
        }
        public static string Error_Input_Menu
        {
            get { return Resources.error_input_menu; }
        }
        public static string Loaded_From_Local
        {
            get { return Resources.loaded_local; }
        }
        public static string Internet_Connection
        {
            get { return Resources.check_internet_connection; }
        }
        public static string Internet_Settings
        {
            get { return Resources.check_internet_settings; }
        }
        public static string Loaded_From_Api
        {
            get { return Resources.loaded_api; }
        }
        public static string Message_Image_Content
        {
            get { return Resources.message_image_content; }
        }
        public static string Message_Image
        {
            get { return Resources.message_image; }
        }
        public static string Message_Not_Select_Rock
        {
            get { return Resources.message_not_select_rock; }
        }
        public static string Yes
        {
            get { return Resources.yes; }
        }
        public static string Not
        {
            get { return Resources.not; }
        }
        public static string Accept
        {
            get { return Resources.accept; }
        }
        public static string Warning
        {
            get { return Resources.warning; }
        }
        public static string Welcome
        {
            get { return Resources.welcome; }
        }
        public static string SedimentaryRocks
        {
            get { return Resources.SedimentaryRocks; }
        }
        public static string MetamorphicRocks
        {
            get { return Resources.MetamorphicRocks; }
        }
        public static string IgneousRocks
        {
            get { return Resources.IgneousRocks; }
        }
        public static string pSelectRock
        {
            get { return Resources.picker_select_the_rock; }
        }
        public static string lName
        {
            get { return Resources.label_name; }
        }
        public static string lDescription
        {
            get { return Resources.label_description; }
        }
        public static string lMinerals
        {
            get { return Resources.label_minerals; }
        }
        public static string lImage
        {
            get { return Resources.label_image; }
        }

        public static string btnShow
        {
            get { return Resources.btnShow; }
        }
        public static string btnExit
        {
            get { return Resources.btnExit; }
        }
        public static string ErrorEmailEmpty
        {
            get { return Resources.error_input_email_0;  }
        }
        public static string ErrorPasswordEmpty
        {
            get { return Resources.error_input_password_0; }
        }
        public static string ErrorResponseNotFound
        {
            get
            {
                return Resources.error_service_no_available;
            }
        }
        public static string ErrorInputCategory
        {
            get
            {
                return Resources.error_input_category;
            }
        }
    }
}
