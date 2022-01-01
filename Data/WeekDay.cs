using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrAcademy.Data
{
    public class WD
    {
        public int DayIndex { get; set; }
        public string DayEn { get; set; }
        public string DayAr { get; set; }
        public WD(int _DayIndex, string _DayEn, string _DayAr)
        {
            DayIndex = _DayIndex;
            DayEn = _DayEn;
            DayAr = _DayAr;
        }
    }

    public static class GetDaysList
    {
        public static List<WD> GetDays()
        {
            List<WD> WkDays = new List<WD>();
            WD D0 = new WD(0, "Monday", "الإثنين");
            WD D1 = new WD(2, "Tuesday", "الثلاثاء");
            WD D2 = new WD(3, "Wednesday", "الأربعاء");
            WD D3 = new WD(4, "Thursday", "الخميس");
            WD D4 = new WD(5, "Friday", "الجمعة");
            WD D5 = new WD(6, "Saturday", "السبت");
            WD D6 = new WD(7, "Sunday", "الأحد");
            WkDays.Add(D0);
            WkDays.Add(D1);
            WkDays.Add(D2);
            WkDays.Add(D3);
            WkDays.Add(D4);
            WkDays.Add(D5);
            WkDays.Add(D6);
            return WkDays;
        }
    }
    public static class TransferDate
    {
        public static string ToArabicDate(DateTime EngDate)
        {
            int day = EngDate.Day;
            string month = GetArabicMonth( EngDate.Month);
            int year = EngDate.Year;
            return $"{day}-{month}-{year}";
        }
        public static string ToStandardArabicDate(DateTime EngDate)
        {
            int day = EngDate.Day;
            string month = GetArabicMonth( EngDate.Month);
            int year = EngDate.Year;
            return $"{day}/{EngDate.Month}/{year}";
        }
        public static string ToArabicDateShortened(DateTime EngDate)
        {
            int day = EngDate.Day;
            string month = GetArabicMonth(EngDate.Month);
            int year = EngDate.Year;
            return $"{month}-{year}";
        }
        private static string GetArabicMonth(int m)
        {
            switch (m)
            {
                case 1:
                    return "يناير";
                    break;
                case 2:
                    return "فبراير";
                    break;
                case 3:
                    return "مارس";
                    break;
                case 4:
                    return "ابريل";
                    break;
                case 5:
                    return "مايو";
                    break;
                case 6:
                    return "يونيو";
                    break;
                case 7:
                    return "يوليو";
                    break;
                case 8:
                    return "اغسطس";
                    break;
                case 9:
                    return "سبتمبر";
                    break;
                case 10:
                    return "اكتوبر";
                    break;
                case 11:
                    return "نوفمبر";
                    break;
                case 12:
                    return "ديسمبر";
                    break;
                default:
                    return "خطأ";

            }
        }
    }
    public static class WeekDay
    {
        public static string WkDayArabic(int DayIndex)
        {

            return GetDaysList.GetDays().Single(m => m.DayIndex == DayIndex).DayAr;
        }

        public static string WkDayEn(int DayIndex)
        {

            return GetDaysList.GetDays().Single(m => m.DayIndex == DayIndex).DayEn;
        }
        public static int WkDayIndexByArabicName(string DayAr)
        {

            return GetDaysList.GetDays().Single(m => m.DayAr == DayAr).DayIndex;
        }
        public static int WkDayIndexByEnglishName(string DayEn)
        {

            return GetDaysList.GetDays().Single(m => m.DayEn == DayEn).DayIndex;
        }

    }


}
