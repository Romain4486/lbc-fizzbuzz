using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace FizzBuzz.Domain.Entities
{
    public class FizzBuzzModel
    {
        public FizzBuzzModel()
        { }

        /// <summary>
        /// Allow to build a fizzbuzz model from the database
        /// </summary>
        /// <param name="primaryKey">use the primary key to fill all the properties of FizzBuzz model</param>
        /// <param name="hits">number of hits compute for a dedicated sequence of fizzbuzz</param>
        public FizzBuzzModel(string primaryKey, int hits)
        {
            const int NB_PARAMETERS_TO_BUILD_FIZZBUZZ = 5;

            var primaryKeyTab = primaryKey.Split('_');
            if (primaryKeyTab != null && primaryKeyTab.Any() && primaryKeyTab.Length == NB_PARAMETERS_TO_BUILD_FIZZBUZZ)
            {
                var parameter = new
                {
                    FizzLabel = 0,
                    FizzNumber = 1,
                    BuzzLabel = 2,
                    BuzzNumber = 3,
                    Limit = 4,
                    Hits = 5
                };

                FizzLabel = primaryKeyTab[parameter.FizzLabel];
                FizzNumber = int.TryParse(primaryKeyTab[parameter.FizzNumber], out int fizzNumber) ? fizzNumber : 0;
                BuzzLabel = primaryKeyTab[parameter.BuzzLabel];
                BuzzNumber = int.TryParse(primaryKeyTab[parameter.BuzzNumber], out int buzzNumber) ? buzzNumber : 0;
                Limit = int.TryParse(primaryKeyTab[parameter.Limit], out int limit) ? limit : 0;
                Hits = hits;
            };
        }

        public string PrimaryKey
        {
            get
            {
                return $"{FizzLabel}_{FizzNumber}_{BuzzLabel}_{BuzzNumber}_{Limit}";
            }
        }

        [Required]
        [Range(1, 100)]
        public int FizzNumber { get; set; }

        [Required]
        [Range(1, 100)]
        public int BuzzNumber { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Limit { get; set; }

        [Required]
        [StringLength(20)]
        public string FizzLabel { get; set; }

        [Required]
        [StringLength(20)]
        public string BuzzLabel { get; set; }

        public int Hits { get; set; }
    }
}
