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
        public float? WORK_ZONE_RELATED { get; set; }
        public float? PEDESTRIAN_INVOLVED { get; set; }
        public float? BICYCLIST_INVOLVED { get; set; }
        public float? MOTORCYCLE_INVOLVED { get; set; }
        public float? IMPROPER_RESTRAINT { get; set; }
        public float? UNRESTRAINED { get; set; }
        public float? DUI { get; set; }
        public float? INTERSECTION_RELATED { get; set; }
        public float? WILD_ANIMAL_RELATED { get; set; }
        public float? DOMESTIC_ANIMAL_RELATED { get; set; }
        public float? OVERTURN_ROLLOVER { get; set; }
        public float? COMMERCIAL_MOTOR_VEH_INVOLVED { get; set; }
        public float? TEENAGE_DRIVER_INVOLVED { get; set; }
        public float? OLDER_DRIVER_INVOLVED { get; set; }
        public float? NIGHT_DARK_CONDITION { get; set; }
        public float? SINGLE_VEHICLE { get; set; }
        public float? DISTRACTED_DRIVING { get; set; }
        public float? DROWSY_DRIVING { get; set; }
        public float? ROADWAY_DEPARTURE { get; set; }

        public Tensor<float?> AsTensor()
        {
            float?[] data = new float?[]
            {
                MILEPOINT, LAT_UTM_Y, LONG_UTM_X, WORK_ZONE_RELATED, UNRESTRAINED, DUI,
                INTERSECTION_RELATED, WILD_ANIMAL_RELATED, OVERTURN_ROLLOVER, COMMERCIAL_MOTOR_VEH_INVOLVED,
                TEENAGE_DRIVER_INVOLVED, OLDER_DRIVER_INVOLVED, NIGHT_DARK_CONDITION, SINGLE_VEHICLE,
                DISTRACTED_DRIVING, ROADWAY_DEPARTURE, CRASH_SEVERITY_ID
            };

            int[] dimensions = new int[] { 1, 17 };

            return new DenseTensor<float?>(data, dimensions);
        }
    }
}
