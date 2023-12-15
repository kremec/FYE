namespace FYE.DataMethods
{
    public static class OmreznineMetode
    {
        public static int Blok(DateTime čas)
        {
            // Nižja sezona
            if (čas.Month >= 3 && čas.Month <= 10)
            {
                // Dela prost dan
                if (DelaProstDan(čas))
                {
                    if (BetweenHours(čas, 0, 6))
                        return 5;
                    else if (BetweenHours(čas, 6, 7))
                        return 4;
                    else if (BetweenHours(čas, 7, 14))
                        return 3;
                    else if (BetweenHours(čas, 14, 16))
                        return 4;
                    else if (BetweenHours(čas, 16, 20))
                        return 3;
                    else if (BetweenHours(čas, 20, 22))
                        return 4;
                    else if (BetweenHours(čas, 22, 24))
                        return 5;
                }
                // Delovni Dan
                else
                {
                    if (BetweenHours(čas, 0, 6))
                        return 4;
                    else if (BetweenHours(čas, 6, 7))
                        return 3;
                    else if (BetweenHours(čas, 7, 14))
                        return 2;
                    else if (BetweenHours(čas, 14, 16))
                        return 3;
                    else if (BetweenHours(čas, 16, 20))
                        return 2;
                    else if (BetweenHours(čas, 20, 22))
                        return 3;
                    else if (BetweenHours(čas, 22, 24))
                        return 4;
                }
            }
            // Višja sezona
            else
            {
                // Dela prost dan
                if (DelaProstDan(čas))
                {
                    if (BetweenHours(čas, 0, 6))
                        return 4;
                    else if (BetweenHours(čas, 6, 7))
                        return 3;
                    else if (BetweenHours(čas, 7, 14))
                        return 2;
                    else if (BetweenHours(čas, 14, 16))
                        return 3;
                    else if (BetweenHours(čas, 16, 20))
                        return 2;
                    else if (BetweenHours(čas, 20, 22))
                        return 3;
                    else if (BetweenHours(čas, 22, 24))
                        return 4;
                }
                // Delovni Dan
                else
                {
                    if (BetweenHours(čas, 0, 6))
                        return 3;
                    else if (BetweenHours(čas, 6, 7))
                        return 2;
                    else if (BetweenHours(čas, 7, 14))
                        return 1;
                    else if (BetweenHours(čas, 14, 16))
                        return 2;
                    else if (BetweenHours(čas, 16, 20))
                        return 1;
                    else if (BetweenHours(čas, 20, 22))
                        return 2;
                    else if (BetweenHours(čas, 22, 24))
                        return 3;
                }
            }
            return -1;
        }
        private static bool BetweenHours(DateTime čas, int min, int max)
        {
            return ((čas.Hour >= min && čas.Hour <= max - 1) || (čas.Hour == max && čas.Minute == 0)) ? true : false;
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
            return statičniDelaProstiDnevi.Any(d => d.Month == x.Month && d.Day == x.Day)
                || dinamičniDelaProstiDnevi.Any(d => d.Date == x.Date)
                || x.DayOfWeek == DayOfWeek.Saturday || x.DayOfWeek == DayOfWeek.Sunday;
        }

        public static double CenaPresežkov(int blok, List<double> presežki)
        {
            double vsotaKvadratovPresežkov = 0;
            foreach (double presežek in presežki)
                vsotaKvadratovPresežkov += presežek * presežek;

            return Math.Sqrt(vsotaKvadratovPresežkov) * faktorUtežitve * (TarifaPrenosniSistem(blok) + TarifaDistribucijskiSistem(blok));
        }
        private static double faktorUtežitve = 0.9;
        private static double TarifaPrenosniSistem(int blok)
        {
            switch(blok)
            {
                case 1: return 0.24923;
                case 2: return 0.04877;
                case 3: return 0.01103;
                case 4: return 0.00038;
                case 5: return 0;
                default: return -1;
            }
        }
        private static double TarifaDistribucijskiSistem(int blok)
        {
            switch (blok)
            {
                case 1: return 3.36401;
                case 2: return 0.83363;
                case 3: return 0.18034;
                case 4: return 0.01278;
                case 5: return 0;
                default: return -1;
            }
        }
    }
}
