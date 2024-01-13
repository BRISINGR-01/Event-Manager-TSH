using SQL_Query_Builder.Interfaces;

namespace Mocks.SQL
{
    public class EnumConverterMock : IEnumToStringConverter
    {
        public string Convert(Enum @enum)
        {
            return @enum.ToString();
        }
    }
}
