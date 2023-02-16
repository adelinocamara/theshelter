using myshelter_api.Models.dto;
using System.Numerics;
using System.Reflection.Emit;

namespace myshelter_api.Data
{
    public static class enterprisestore
    {
        public static List<enterprisedto> enterpriseList = new List<enterprisedto>
        {
                new enterprisedto{Id=1,Name="Casa de Refugio",ZipCode="33178", Phone="9548597075"},
                new enterprisedto{Id=2,Name="Mas Que Vencedores",ZipCode="32120", Phone="3501689754"}
            };
    }
}
