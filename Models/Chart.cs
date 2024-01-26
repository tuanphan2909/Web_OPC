        using System;
using System.Runtime.Serialization;

namespace ASPNET_MVC_ChartsDemo.Models
{
    //DataContract for Serializing Data - required to serve in JSON format
    [DataContract]
    public class Chart
    {
        public Chart(string label, int y)
        {
            this.Label = label;
            this.Y = y;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "label")]
        public string Label = "";

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<double> Y = null;
    }
    public class StudentMarkDetails
    {
        public int id { get; set; }
        public string name { get; set; }
        public int Physics { get; set; }
        public int Chemistry { get; set; }
        public int Biology { get; set; }
        public int Mathematics { get; set; }


    }

    public class B20DmDvt
    {
        public string Ma_Dvcs { get; set; }
        public string tendangnhap { get; set; }
        public string hoten { get; set; }
       
        public string Ten_Dvcs { get; set; }
        // Các thuộc tính khác tương ứng với cột trong bảng B20DmDvt
    }

}