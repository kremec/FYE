namespace FYE
{
    public class OmreznineMetode
    {        
        public int PridobiBlokZaČas(DateTime čas)
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
        }

        private bool Between(int x, int min, int max)
        {
            return (x >= min && x <= max);
        }
        private bool DelaProstDan(DateTime x)
        {
            List<DateTime> delaProstiDnevi = new List<DateTime>()
            {
                new DateTime(0, 1, 1), // novo leto
                new DateTime(0, 1, 2), // novo leto
                new DateTime(0, 2, 8), // Prešernov dan
                new DateTime(0, 4, 27), // dan upora proti okupatorju
                new DateTime(0, 5, 1), // praznik dela
                new DateTime(0, 5, 2), // praznik dela
                new DateTime(0, 6, 25), // dan državnosti
                new DateTime(0, 15, 8), // Marijino vnebovzetje
                new DateTime(0, 11, 1), // dan spomina na mrtve
                new DateTime(0, 12, 25), // božič
                new DateTime(0, 12, 26) // dan samostojnosti in enotnosti
                /*
                 * velikonočna nedelja, velikonočni ponedeljek
                 * binkoštna nedelja - binkošti
                 */
            };
            return delaProstiDnevi.Any(d => d.Month == x.Month && d.Day == x.Day);
        }
    }
}
