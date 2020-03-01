namespace Lab.Entities
{
    public class Employee
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Role Role { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            //序列畫 反序列畫 加減密 壓縮解壓縮 比較會消耗效能

            //常用在寫 logger 沒用過表示怪怪的
            return $"{nameof(LastName)}: {LastName}, {nameof(FirstName)}: {FirstName}, {nameof(Role)}: {Role}, {nameof(Age)}: {Age}";
        }
    }
}