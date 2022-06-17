using LaptopStore.Data.Interfaces;
using LaptopStore.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopStore.Data.Mocks
{
    public class MockLaptops
    {
        private readonly MockLaptopCategories _laptopCategories = new MockLaptopCategories();
        public IEnumerable<Laptop> getLaptops
        {
            get
            {
                return new List<Laptop>
                {
                    new Laptop
                    {
                        id = 1,
                        name = "ROG Strix G15/17",
                        categoryId = 4,
                        Category = _laptopCategories.AllCategories.Single(c => c.categoryName == "Gaming"),
                        isAvailable = true,
                        isFavorite = true,
                        price = 3000,
                        imgUrl = "/Img/ROG Strix G15_17.png",
                        shortDesc = "ROG Strix G15/17 is suitable for a wide range of tasks.",
                        longDesc = "With a powerful configuration that includes an AMD Ryzen 9 5900HX processor " +
                                    "and an AMD Radeon RX 6800M graphics card, this laptop is suitable for a wide range of tasks." +
                                    "Optimized cooling system ensures stable operation of the device under heavy " +
                                    "loads, so ROG Strix G15 will allow you to show all your skills in any game" +
                                    " situations! The built-in speaker system of the laptop consists of two speakers with " +
                                    "intelligent amplification aimed directly at the user. When connected " +
                                    "headphones, it will create a fantastic 5.1.2-channel sound effect based on technology" +
                                    "Dolby Atmos and will allow you to fully immerse yourself in what is happening on the screen. To enhance " +
                                    "voice quality is served by two-way intelligent noise reduction technology." +
                                    " The 90 Wh battery delivers long battery life."
                    },
                    new Laptop
                    {
                        id = 2,
                        name = "MSI MODERN 14 B10MW-294XRU",
                        categoryId = 1,
                        Category = _laptopCategories.AllCategories.Single(c => c.categoryName == "Ultrathin"),
                        isAvailable = false,
                        isFavorite = true,
                        price = 666,
                        imgUrl = "/Img/MSI MODERN 14 B10MW-294XRU.jpg",
                        shortDesc = "Do you need a trouble-free assistant at work? It is in front of you! This is MSI MODERN 14 B10MW-294XRU Ultrabook.",
                        longDesc = "Do you need a trouble-free assistant at work? It is in front of you! This is MSI MODERN 14 B10MW-294XRU Ultrabook." +
                                    "Its matte 14-inch screen displays a high resolution (1920x1080 pix.) image. " +
                                    "Thanks to the Intel Core i3 processor and a 256 GB SSD, it got decent performance. " +
                                    "The operating time of the model without connecting to a power outlet reaches 10 hours. In combination with the ability to receive " +
                                    "Internet at high speed thanks to Wi-Fi module 5 (802.11ac) which means that you can work with an ultrabook" +
                                    "in a cafe or other suitable place, without worrying about the battery level. The classic black laptop case is made" +
                                    "made of plastic and metal. For the convenience of working with text files and tables in any environment, ultrabook " +
                                    "MSI MODERN 14 B10MW-294XRU has a built-in backlight."
                    },
                    new Laptop
                    {
                        id = 3,
                        name = "Lenovo IdeaPad Flex 5 14ALC05",
                        categoryId = 2,
                        Category = _laptopCategories.AllCategories.Single(c => c.categoryName == "Transformer"),
                        isAvailable = false,
                        isFavorite = true,
                        price = 1050,
                        imgUrl = "/Img/Lenovo IdeaPad Flex 5 14ALC05.jpg",
                        shortDesc = "IdeaPad Flex 5 14 inch laptop with AMD Ryzen mobile processor delivers high " +
                                     "Performance and rich graphics in a flexible device",
                        longDesc = "IdeaPad Flex 5 14 inch laptop with AMD Ryzen mobile processor delivers high " +
                                     "Performance and rich graphics in a flexible device that you can use just the way you want." +
                                     "The thin bezels that surround the IdeaPad Flex 5 screen on all sides give this device a stylish look and also expand" +
                                     "users' workspace, allowing them to fully enjoy the quality of an FHD display."
                    },
                    new Laptop
                    {
                        id = 4,
                        name = "Irbis NB77",
                        categoryId = 3,
                        Category = _laptopCategories.AllCategories.Single(c => c.categoryName == "Office"),
                        isAvailable = true,
                        isFavorite = false,
                        price = 233,
                        imgUrl = "/Img/Irbis NB77.jpg",
                        shortDesc = "The Irbis NB77 laptop has everything you need for convenient and productive work both in the office and while traveling.",
                        longDesc = "The Irbis NB77 laptop has everything you need for convenient and productive work like in the office, " +
                                     "and while traveling. It is made in a case with compact dimensions and a weight of 1.3 kg. " +
                                     "Thanks to the 13.3-inch HD screen, a realistic picture is displayed with high " +
                                     "detailed and rich colors. The audio system with two speakers reproduces a clear sound."
                    },
                    new Laptop
                    {
                        id = 5,
                        name = "HP Laptop 14s-fq0111ur",
                        categoryId = 1,
                        Category = _laptopCategories.AllCategories.Single(c => c.categoryName == "Ultrathin"),
                        isAvailable = true,
                        isFavorite = false,
                        price = 566,
                        imgUrl = "/Img/HP Laptop 14s-fq0111ur.jpg",
                        shortDesc = "Stylish, compact laptop with thin bezels and long battery life",
                        longDesc = "Stylish, compact laptop with thin bezels and long battery life" +
                                     "allows you to always stay connected. A reliable HP laptop with a 14-inch screen allows you to " +
                                     "work in the browser, stream and quickly perform many other tasks. Consistent productivity for " +
                                     "work and play, wherever you are."
                    },
                    new Laptop
                    {
                        id = 6,
                        name = "Digma EVE 15 C407",
                        categoryId = 3,
                        Category = _laptopCategories.AllCategories.Single(c => c.categoryName == "Office"),
                        isAvailable = true,
                        isFavorite = false,
                        price = 333,
                        imgUrl = "/Img/Digma EVE 15 C407.jpg",
                        shortDesc = "Digma EVE 15 C407 is designed for those who want a reliable and productive computing device",
                        longDesc = "Digma EVE 15 C407 is designed for those who want a reliable and productive computing device" +
                                    "with the most requested functionality. This model fully satisfies these requirements. " +
                                    "Reliable storage gives you long-term storage options for the virtual information you need. " +
                                    "The device is equipped with a webcam and a microphone, thanks to which you can organize video conferences" +
                                    "with business partners and work colleagues."
                    },
                    new Laptop
                    {
                        id = 7,
                        name = "ASUS Vivobook 13 Slate OLED T3300KA-LQ084W",
                        categoryId = 2,
                        Category = _laptopCategories.AllCategories.Single(c => c.categoryName == "Transformer"),
                        isAvailable = true,
                        isFavorite = true,
                        price = 1150,
                        imgUrl = "/Img/ASUS Vivobook 13 Slate OLED T3300KA-LQ084W.jfif",
                        shortDesc = "The Vivobook 13 Slate OLED, a fantastic convertible laptop designed for the mobile lifestyle.",
                        longDesc = "The Vivobook 13 Slate OLED is a fantastic convertible laptop designed for the mobile lifestyle." +
                                    "He easily adapts to whatever his owner does, whether it's work or school, leisure or socializing. " +
                                    "Support for ASUS Pen 2.01 with interchangeable nibs of different types will appeal to all creative minds, " +
                                    "and entertainment lovers will appreciate the quality image on the luxurious OLED display with Dolby Vision technology, " +
                                    "complemented by the mesmerizing sound of the built-in audio system with Dolby Atmos spatial effects."
                    },
                    new Laptop
                    {
                        id = 8,
                        name = "Acer Nitro 5 AN515-55-534C",
                        categoryId = 4,
                        Category = _laptopCategories.AllCategories.Single(c => c.categoryName == "Gaming"),
                        isAvailable = false,
                        isFavorite = true,
                        price = 1333,
                        imgUrl = "/Img/Acer Nitro 5 AN515-55-534C.jpg",
                        shortDesc = "The Acer Nitro 5 AN515-55-534C has a powerful hardware configuration that makes it the perfect gaming solution.",
                        longDesc = "The Acer Nitro 5 AN515-55-534C has a powerful hardware configuration that makes it the perfect gaming solution." +
                                    "The 15.6-inch model uses IPS technology, which allows you to get such a realistic image, " +
                                    "that the line between the virtual world and reality will involuntarily be erased, and you will be completely immersed in the gameplay." +
                                    "The picture acquires a resolution of 1920x1080, retains clarity, natural colors and brightness. Online games on this device" +
                                    "will not allow connection failures, because the Wi-Fi module is responsible for this. You will not notice a discrepancy between your actions" +
                                    "and display graphics on the screen, since the device uses the GeForce RTX 3050 video processor, which features 4 GB of memory. " +
                                    "The hardware power of the model is represented by the DDR4 module interacting with the Intel Core i5 10300H processor."
                    }

                };
            }
        }

        public IEnumerable<Laptop> getFavLaptops { get; set; }
    }
}
