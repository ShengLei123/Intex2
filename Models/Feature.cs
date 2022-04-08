using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace Intex2.Models
{
    public class Feature
    {
        [Key]
        [Required]
        public int CRASH_ID { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Milepoint must be between 1 and 100")]
        public float MILEPOINT { get; set; }

        [Required(ErrorMessage = "Latitude must be specified")]
        public float LAT_UTM_Y { get; set; }

        [Required(ErrorMessage = "Longitude must be specified")]
        public float LONG_UTM_X { get; set; }

        [Required(ErrorMessage = "This field must be specified")]
        public float WORK_ZONE_RELATED { get; set; }

        [Required(ErrorMessage = "This field must be specified")]
        public float UNRESTRAINED { get; set; }

        [Required(ErrorMessage = "This field must be specified")]
        public float DUI { get; set; }

        [Required(ErrorMessage = "This field must be specified")]
        public float INTERSECTION_RELATED { get; set; }

        [Required(ErrorMessage = "This field must be specified")]
        public float WILD_ANIMAL_RELATED { get; set; }

        [Required(ErrorMessage = "This field must be specified")]
        public float OVERTURN_ROLLOVER { get; set; }

        [Required(ErrorMessage = "This field must be specified")]
        public float COMMERCIAL_MOTOR_VEH_INVOLVED { get; set; }

        [Required(ErrorMessage = "This field must be specified")]
        public float TEENAGE_DRIVER_INVOLVED { get; set; }

        [Required(ErrorMessage = "This field must be specified")]
        public float OLDER_DRIVER_INVOLVED { get; set; }

        [Required(ErrorMessage = "This field must be specified")]
        public float NIGHT_DARK_CONDITION { get; set; }

        [Required(ErrorMessage = "This field must be specified")]
        public float SINGLE_VEHICLE { get; set; }

        [Required(ErrorMessage = "This field must be specified")]
        public float DISTRACTED_DRIVING { get; set; }

        [Required(ErrorMessage = "This field must be specified")]
        public float ROADWAY_DEPARTURE { get; set; }

        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
                MILEPOINT, LAT_UTM_Y, LONG_UTM_X, WORK_ZONE_RELATED,
                UNRESTRAINED, DUI,
                INTERSECTION_RELATED, WILD_ANIMAL_RELATED, OVERTURN_ROLLOVER, COMMERCIAL_MOTOR_VEH_INVOLVED,
                TEENAGE_DRIVER_INVOLVED, OLDER_DRIVER_INVOLVED, NIGHT_DARK_CONDITION, SINGLE_VEHICLE, DISTRACTED_DRIVING,
                ROADWAY_DEPARTURE
            };
            int[] dimensions = new int[] { 1, 16 };

            return new DenseTensor<float>(data, dimensions);
        }
    }
}
