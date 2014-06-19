using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tenlaruen
{
    class DataSetMNIST
    {
        ImageMNIST[] trainingData;
        ImageMNIST[] testData;


        public DataSetMNIST()
        {
            trainingData = new ImageMNIST[60000];
            testData = new ImageMNIST[10000];
        }

        public ImageMNIST GetTestImage(uint index)
        {
            return testData[index];
        }

        public void LoadTestData()
        {
            try
            {
                FileStream ifsLabels = new FileStream(@"t10k-labels.idx1-ubyte", FileMode.Open);
                FileStream ifsImages = new FileStream(@"t10k-images.idx3-ubyte", FileMode.Open);

                BinaryReader brLabes = new BinaryReader(ifsLabels);
                BinaryReader brImages = new BinaryReader(ifsImages);

                int magic1 = brImages.ReadInt32();
                int numImages = brImages.ReadInt32();
                int numRows = brImages.ReadInt32();
                int numCols = brImages.ReadInt32();

                int magic2 = brLabes.ReadInt32();
                int numLabel = brLabes.ReadInt32();

                byte[][] pixels = new byte[28][];
                for (int i = 0; i < pixels.Length; ++i)
                    pixels[i] = new byte[28];


                for (int di = 0; di < 10000; ++di)
                {
                    for (int i = 0; i < 28; ++i)
                    {
                        for (int j = 0; j < 28; ++j)
                        {
                            byte b = brImages.ReadByte();
                            pixels[i][j] = b;
                        }
                    }

                    byte lbl = brLabes.ReadByte();

                    testData[di] = new ImageMNIST(pixels, lbl);
                }

                ifsImages.Close();
                brImages.Close();
                ifsLabels.Close();
                brLabes.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        public void LoadTrainingData()
        {
            try
            {
                FileStream ifsLabels = new FileStream(@"train-labels.idx1-ubyte", FileMode.Open);
                FileStream ifsImages = new FileStream(@"train-images.idx3-ubyte", FileMode.Open);

                BinaryReader brLabes = new BinaryReader(ifsLabels);
                BinaryReader brImages = new BinaryReader(ifsImages);

                int magic1 = brImages.ReadInt32();
                int numImages = brImages.ReadInt32();
                int numRows = brImages.ReadInt32();
                int numCols = brImages.ReadInt32();

                int magic2 = brLabes.ReadInt32();
                int numLabel = brLabes.ReadInt32();

                byte[][] pixels = new byte[28][];
                for (int i = 0; i < pixels.Length; ++i)
                    pixels[i] = new byte[28];

                for(int di = 0; di < 60000; ++di)
                {
                    for(int i = 0; i < 28; ++i)
                    {
                        for(int j = 0; j < 28; ++j)
                        {
                            byte b = brImages.ReadByte();
                            pixels[i][j] = b;
                        }                     
                    }

                    byte lbl = brLabes.ReadByte();

                    trainingData[di] = new ImageMNIST(pixels, lbl);
                }

                ifsImages.Close();
                brImages.Close();
                ifsLabels.Close();
                brLabes.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        
    }
}
