namespace Casus_Programmeren;

public class Equations
{
    
    // ---------------------------
    // Equations for Question 5
    // ---------------------------
    
    public static float calculateCost(Room room, bool dayBasis, int startHour)
    {
        float totalCost = 0;

        totalCost += addHeatingCost(startHour);
    
        totalCost += addRoombasedCost(room);
        
        totalCost = incorperateTimeBasis(dayBasis, totalCost);
        
        return totalCost;

    }

    private static int addHeatingCost(int hour)
    {
        if (hour > 4) return 0;

        return 6 - hour;
    }

    private static int addRoombasedCost(Room room)
    {
        if (room.roomType == Roomtype.Publiekeruimte) return 200;


        if (room.building == Building.Prisma)
        {
            return prismaCalculations(room); 
        }

        if (room.building == Building.Spectrum)
        {
            return spectrumCalculations(room);
        }
        
        // Default, we assume it's somewhere outside. 
        return 0;
    }

    private static int prismaCalculations(Room room)
    {
        
        if (room.roomType == Roomtype.Werkruimte) return 90;
     
        // 27 cap
        if (room.capacity < 28)
        {
            return 275;
        }
        
        // higher than 27 cap
        return 475;
    }
    
    private static int spectrumCalculations(Room room)
    {
        
        if (room.roomType == Roomtype.Werkruimte) return 100;
     
        // 27 cap
        if (room.capacity < 28)
        {
            return 300;
        }
        
        // higher than 27 cap
        return 500;
    }

    private static float incorperateTimeBasis(bool dayBasis, float price)
    {
        // We assume that a week is a full week, not a work week. Mr Eugene Lewis Fordsworthe rolling in his grave
        if (!dayBasis) return price * 7;

        return price;
    }
}