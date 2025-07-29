namespace com.rorisoft.utils
{
    public class DateUtils
    {
        public static long EpochMsecs()
        {
            return new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
        }

        public static (double units, string uom) Diff2Dates(DateTime? startDate, DateTime? endDate)
        {
            double delta = 0;
            string uom = "";

            if (startDate.HasValue && endDate.HasValue)
            {
                TimeSpan ts = (endDate.Value - startDate.Value);


                if (ts.Days == 0)
                {
                    if (ts.Hours == 0)
                    {
                        if (ts.Minutes == 0)
                        {
                            delta = ts.Seconds;
                            uom = "segs";
                        } else { 
                            delta = ts.Minutes;
                            uom = "mins";
                        }
                    }
                    else
                    {
                        delta = ts.Hours;
                        uom = "horas";
                    }
                }
                else
                {
                    delta = ts.Days;
                    uom = "días";
                }

            }
            return (delta, uom);
        }
    }
}
