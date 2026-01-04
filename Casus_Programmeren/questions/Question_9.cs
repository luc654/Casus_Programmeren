using System.Globalization;

namespace Casus_Programmeren.questions;

// 9. Als beheerder wil ik inzage in de bezettingsgraad over een maand van de gebouwen
// Spectrum en Prisma.

public class Question_9
{
    private static terminalHelper helper = new terminalHelper();

    public static void calculateOccupancyRate()
    {
        List<string> terugKnop = new List<string>(){"Terug"};

        int month = getMonth();
        List<string> dates = GenerateDaysList(month);
        int totalAmountOfReservationsMonthly = getTotalAmountOfReservationsMonthly(dates);

        int roomCount  = getTotalCountOfRooms();

        if (roomCount == 0)
        {
            Program.GlobalContext.notification = "Geen ruimtes om te berekenen";
            return;
        }
        
        float result = calculate(totalAmountOfReservationsMonthly, dates, roomCount);

        string formattedText = $"""
                                Bezettingsgraad van maand {month} is {result}%
                                """;
        helper.handleTerminal(terugKnop, "Bezettingsgraad berekenen", formattedText);

    }

    /// <summary>
    /// Prompts the user 12 months, returns the index of selected month, e.g. januari = 1
    /// </summary>
    /// <returns></returns>
    private static int getMonth()
    {
        List<string> months = new List<string>()
        {  "januari",  "februari",  "maart",  "april",  "mei",  "juni",  "juli",  "augustus",  "september",  "oktober",  "november", "december" };
        
        // plus 1 for 0 based index offset
        int monthIndex = helper.handleTerminal(months, "Bezettingsgraad berekenen", "Selecteer een maand") + 1;
        return monthIndex;
    }

    /// <summary>
    /// Inputs a int month and returns a list of string dates in yyyy-MM-dd format from the given month.
    /// </summary>
    /// <param name="month"></param>
    /// <returns></returns>
    private static List<string> GenerateDaysList(int month)
    {
        int currentYear = DateTime.Now.Year;
        int daysInMonth = DateTime.DaysInMonth(currentYear, month);

        List<string> dates = new List<string>();

        for (int day = 1; day <= daysInMonth; day++)
        {
            DateTime date = new DateTime(currentYear, month, day);
            dates.Add(date.ToString("yyyy-MM-dd"));
        }

        return dates;
    }


    /// <summary>
    /// Inputs a List of string dates in yyyy-MM-dd format, returns an integer of total amount of reservations that month.
    /// </summary>
    /// <param name="dates"></param>
    /// <returns></returns>
    private static int getTotalAmountOfReservationsMonthly(List<string> dates)
    {
        int totalAmount = 0;
        ReservationHelper reservationHelper = new ReservationHelper();

        foreach (string date in dates)
        {
            List<Reservation> reservations = reservationHelper.getReservationsFromDate(date);
            
            // Remove duplicate reservations based on roomNumber. Since each room should only be counted once. fun error to fix.
            List<Reservation> filteredReservations = reservations.DistinctBy(x => x.roomNumber).ToList();
            totalAmount += filteredReservations.Count;
        }


        
        return totalAmount;
    }

    private static int getTotalCountOfRooms()
    {
        return Program.GlobalContext.Rooms.getRooms().Count;
    }

    private static float calculate(int totalOccupancy, List<string> dates, int roomCount)
    {
        int days = dates.Count;

        // Avoid divide by 0 error
        if (totalOccupancy == 0) return 0;
        
        // Cast to float to prevent integer division data loss.
        // https://stackoverflow.com/questions/10851273/why-does-integer-division-in-c-sharp-return-an-integer-and-not-a-float
        float result = ((float)totalOccupancy / (days * roomCount)) * 100;
        return result;
    }
}