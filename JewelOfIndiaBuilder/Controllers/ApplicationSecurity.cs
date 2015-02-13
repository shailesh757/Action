namespace JewelOfIndiaBuilder.Controllers
{
    public class ApplicationSecurity
    {
        public static bool CheckUser(string userType, string idAdmin,string viewType)
        {
            if(idAdmin.ToUpper()=="TRUE")
                return true;
            if (userType.ToUpper() == "SUBADMIN" && viewType.ToUpper() != "APARTMENTSALES")
            {
                return true;
            }
            if(userType.ToUpper() == "MARKETINGADMIN" && viewType.ToUpper()=="REPORT")
                return true;

            return false;

        }
    }
}
