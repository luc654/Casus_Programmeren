using Xunit;
// Note, unit tests made by gpt to test Equations.cs
namespace Casus_Programmeren.Tests
{
    public class EquationsTests
    {
        [Theory]
        // Heating cost test: hour <= 4 adds 6-hour
        [InlineData(0, 6)]
        [InlineData(2, 4)]
        [InlineData(4, 2)]
        [InlineData(5, 0)] // hour > 4 returns 0
        public void AddHeatingCost_CorrectValue(int hour, int expected)
        {
            // Using reflection to call private method for testing
            var method = typeof(Equations)
                .GetMethod("addHeatingCost", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            
            int result = (int)method.Invoke(null, new object[] { hour });
            Assert.Equal(expected, result);
        }

        [Theory]
        // Room-based cost for Publiekeruimte always 200
        [InlineData(Roomtype.Publiekeruimte, Building.PrismA, 10, 200)]
        [InlineData(Roomtype.Publiekeruimte, Building.Spectrum, 50, 200)]
        public void AddRoomBasedCost_Publiekeruimte_Returns200(Roomtype type, Building building, int capacity, int expected)
        {
            var room = new Room { roomType = type, building = building, capacity = capacity };

            var method = typeof(Equations)
                .GetMethod("addRoombasedCost", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

            int result = (int)method.Invoke(null, new object[] { room });
            Assert.Equal(expected, result);
        }

        [Theory]
        // Prisma building calculations
        [InlineData(Roomtype.Werkruimte, 10, 90)]
        [InlineData(Roomtype.Other, 10, 275)]
        [InlineData(Roomtype.Other, 30, 475)]
        public void PrismaCalculations_ReturnsCorrectCost(Roomtype type, int capacity, int expected)
        {
            var room = new Room { roomType = type, building = Building.Prisma, capacity = capacity };

            var method = typeof(Equations)
                .GetMethod("prismaCalculations", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

            int result = (int)method.Invoke(null, new object[] { room });
            Assert.Equal(expected, result);
        }

        [Theory]
        // Spectrum building calculations
        [InlineData(Roomtype.Werkruimte, 10, 100)]
        [InlineData(Roomtype.Other, 10, 300)]
        [InlineData(Roomtype.Other, 30, 500)]
        public void SpectrumCalculations_ReturnsCorrectCost(Roomtype type, int capacity, int expected)
        {
            var room = new Room { roomType = type, building = Building.Spectrum, capacity = capacity };

            var method = typeof(Equations)
                .GetMethod("spectrumCalculations", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

            int result = (int)method.Invoke(null, new object[] { room });
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(true, 100, 100)]
        [InlineData(false, 100, 700)]
        public void IncorporateTimeBasis_ReturnsCorrectValue(bool dayBasis, float price, float expected)
        {
            var method = typeof(Equations)
                .GetMethod("incorperateTimeBasis", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

            float result = (float)method.Invoke(null, new object[] { dayBasis, price });
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(Roomtype.Publiekeruimte, Building.PrismA, 10, true, 206)] // 6 + 200
        [InlineData(Roomtype.Werkruimte, Building.Prisma, 10, false, 1330)] // (6+90)*7
        [InlineData(Roomtype.Other, Building.Spectrum, 30, true, 500)] // 0+500
        [InlineData(Roomtype.Other, Building.Other, 30, false, 0)] // 0*7=0
        public void CalculateCost_ReturnsExpected(Roomtype type, Building building, int capacity, bool dayBasis, float expected)
        {
            var room = new Room { roomType = type, building = building, capacity = capacity };
            float startHour = 0; // This is used for heating, adjust if needed
            if(type==Roomtype.Publiekeruimte) startHour = 0;
            else startHour = 0;

            float result = Equations.calculateCost(room, dayBasis, (int)startHour);
            Assert.Equal(expected, result);
        }
    }
}
