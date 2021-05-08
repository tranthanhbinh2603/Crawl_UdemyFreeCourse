using System;

namespace Chia_phần
{
    class Program
    {
        static void Chiaphan(int n, int sophan, ref int[]kq)
        {
            int ph_nguyen = n / sophan;
            int ph_du = n % sophan;
            int cs = 0;
            int hien_tai = 0;
            for (int i = 1; i <= sophan; i++)
            {
                if (cs < ph_du) 
                {
                    kq[i - 1] = hien_tai + ph_nguyen + 1;
                    cs += 1;
                    hien_tai = kq[i - 1];
                }
                else
                {
                    kq[i - 1] = hien_tai + ph_nguyen;
                    hien_tai = kq[i - 1];
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Nhap so luong phan: ");
            int n = int.Parse(Console.ReadLine());
            int[] kq = new int[100];
            for (int i = 0; i < 100; i++)
            {
                kq[i] = 0;
            }
            Chiaphan(n, 5, ref kq);
            for (int i = 1; i <= 5; i++)
            {
                if (i == 1)
                {
                    Console.WriteLine("{0}->{1}",1, kq[i - 1]);
                }
                else
                {
                    Console.WriteLine("{0}->{1}", kq[i - 2], kq[i - 1]);
                }
            }
        }
    }
}
