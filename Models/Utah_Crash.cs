using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace Intex2.Models
{
    public class Utah_Crash
    {
        [Key]
        [Required]
        public int CRASH_ID { get; set; }
        public DateTime? CRASH_DATETIME { get; set; }
        public string? ROUTE { get; set; }
        public float? MILEPOINT { get; set; }
        public float? LAT_UTM_Y { get; set; }
        public float? LONG_UTM_X { get; set; }
        public string? MAIN_ROAD_NAME { get; set; }
        public string? CITY { get; set; }
        public string? COUNTY_NAME { get; set; }
        public int? CRASH_SEVERITY_ID { get; set; }
        public double? WORK_ZONE_RELATED { get; set; }
        public double? PEDESTRIAN_INVOLVED { get; set; }
        public double? BICYCLIST_INVOLVED { get; set; }
        public double? MOTORCYCLE_INVOLVED { get; set; }
        public double? IMPROPER_RESTRAINT { get; set; }
        public double? UNRESTRAINED { get; set; }
        public double? DUI { get; set; }
        public double? INTERSECTION_RELATED { get; set; }
        public double? WILD_ANIMAL_RELATED { get; set; }
        public double? DOMESTIC_ANIMAL_RELATED { get; set; }
        public double? OVERTURN_ROLLOVER { get; set; }
        public double? COMMERCIAL_MOTOR_VEH_INVOLVED { get; set; }
        public double? TEENAGE_DRIVER_INVOLVED { get; set; }
        public double? OLDER_DRIVER_INVOLVED { get; set; }
        public double? NIGHT_DARK_CONDITION { get; set; }
        public double? SINGLE_VEHICLE { get; set; }
        public double? DISTRACTED_DRIVING { get; set; }
        public double? DROWSY_DRIVING { get; set; }
        public double? ROADWAY_DEPARTURE { get; set; }

        public Tensor<float?> AsTensor()
        {
            float?[] data = new float?[]
            {
                MILEPOINT, LAT_UTM_Y, LONG_UTM_X
            };
            int[] dimensions = new int[] { 1, 3 };

            return new DenseTensor<float?>(data, dimensions);
        }

    }
}
