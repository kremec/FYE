namespace FYE
{
    public static class OmreznineMetode
    {        
        public static int PridobiBlokZaČas(DateTime čas)
        {
            // Nižja sezona
            if (Between(čas.Month, 3, 10))
            {
                // Dela prost dan
                if (DelaProstDan(čas))
                {
                    if (Between(čas.Hour, 0, 6))
                        return 5;
                    else if (Between(čas.Hour, 6, 7))
                        return 4;
                    else if (Between(čas.Hour, 7, 14))
                        return 3;
                    else if (Between(čas.Hour, 14, 16))
                        return 4;
                    else if (Between(čas.Hour, 16, 20))
                        return 3;
                    else if (Between(čas.Hour, 20, 22))
                        return 4;
                    else if (Between(čas.Hour, 22, 23))
                        return 5;
                }
                // Delovni Dan
                else
                {
                    if (Between(čas.Hour, 0, 6))
                        return 4;
                    else if (Between(čas.Hour, 6, 7))
                        return 3;
                    else if (Between(čas.Hour, 7, 14))
                        return 2;
                    else if (Between(čas.Hour, 14, 16))
                        return 3;
                    else if (Between(čas.Hour, 16, 20))
                        return 2;
                    else if (Between(čas.Hour, 20, 22))
                        return 3;
                    else if (Between(čas.Hour, 22, 23))
                        return 4;
                }
            }
            // Višja sezona
            else
            {
                // Dela prost dan
                if (DelaProstDan(čas))
                {
                    if (Between(čas.Hour, 0, 6))
                        return 4;
                    else if (Between(čas.Hour, 6, 7))
                        return 3;
                    else if (Between(čas.Hour, 7, 14))
                        return 2;
                    else if (Between(čas.Hour, 14, 16))
                        return 3;
                    else if (Between(čas.Hour, 16, 20))
                        return 2;
                    else if (Between(čas.Hour, 20, 22))
                        return 3;
                    else if (Between(čas.Hour, 22, 23))
                        return 4;
                }
                // Delovni Dan
                else
                {
                    if (Between(čas.Hour, 0, 6))
                        return 3;
                    else if (Between(čas.Hour, 6, 7))
                        return 2;
                    else if (Between(čas.Hour, 7, 14))
                        return 1;
                    else if (Between(čas.Hour, 14, 16))
                        return 2;
                    else if (Between(čas.Hour, 16, 20))
                        return 1;
                    else if (Between(čas.Hour, 20, 22))
                        return 2;
                    else if (Between(čas.Hour, 22, 23))
                        return 3;
                }
            }
            return -1;
        }

        private static bool Between(int x, int min, int max)
        {
            return (x >= min && x <= max);
        }
        private static bool DelaProstDan(DateTime x)
        {
            List<DateTime> statičniDelaProstiDnevi = new List<DateTime>()
            {
                new DateTime(1, 1, 1), // novo leto
                new DateTime(1, 1, 2), // novo leto
                new DateTime(1, 2, 8), // Prešernov dan
                new DateTime(1, 4, 27), // dan upora proti okupatorju
                new DateTime(1, 5, 1), // praznik dela
                new DateTime(1, 5, 2), // praznik dela
                new DateTime(1, 6, 25), // dan državnosti
                new DateTime(1, 8, 15), // Marijino vnebovzetje
                new DateTime(1, 11, 1), // dan spomina na mrtve
                new DateTime(1, 12, 25), // božič
                new DateTime(1, 12, 26) // dan samostojnosti in enotnosti
            };
            List<DateTime> dinamičniDelaProstiDnevi = new List<DateTime>()
            {
                // Velike noči
                new DateTime(2021, 4, 4),
                new DateTime(2021, 4, 5),
                new DateTime(2022, 4, 17),
                new DateTime(2022, 4, 18),
                new DateTime(2023, 4, 9),
                new DateTime(2023, 4, 10),
                new DateTime(2024, 3, 31),
                new DateTime(2024, 4, 1),
                // Binkošti
                new DateTime(2021, 5, 23),
                new DateTime(2022, 6, 5),
                new DateTime(2023, 5, 28),
                new DateTime(2024, 5, 19)
            };
            return (statičniDelaProstiDnevi.Any(d => d.Month == x.Month && d.Day == x.Day)
                || dinamičniDelaProstiDnevi.Any(d => d.Date == x.Date)
                || x.DayOfWeek == DayOfWeek.Saturday || x.DayOfWeek == DayOfWeek.Sunday);
        }
    }
}
