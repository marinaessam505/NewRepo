namespace assigoop
{

    using static System.Net.Mime.MediaTypeNames;

    #region DeliveryAddress

    struct DeliveryAddress
    {
        public string City;
        public string Country;

        public DeliveryAddress(string city, string country)
        {
            City = city;
            Country = country;
        }
    }

    #endregion

    #region Shipment

    class Shipment
    {
        public string TrackingCode { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public decimal DeliveryFee { get; set; }
        public DeliveryAddress Destination { get; set; }

        public Shipment(string trackingCode, string description,
            decimal weight, decimal deliveryFee,
            DeliveryAddress destination)
        {
            TrackingCode = trackingCode;
            Description = description;
            Weight = weight;
            DeliveryFee = deliveryFee;
            Destination = destination;
        }

        public virtual decimal EstimatedCost
        {
            get { return DeliveryFee + Weight * 5; }
        }

        public void UpdateDeliveryFee(decimal fee)
        {
            DeliveryFee = fee;
        }

        public virtual void PrintShipment()
        {
            Console.WriteLine($"Tracking Code : {TrackingCode}");
            Console.WriteLine($"Description   : {Description}");
            Console.WriteLine($"Weight        : {Weight} KG");
            Console.WriteLine($"Delivery Fee  : {DeliveryFee} EGP");
            Console.WriteLine($"Estimated Cost: {EstimatedCost} EGP");
        }
    }

    #endregion

    #region StandardShipment

    class StandardShipment : Shipment
    {
        public StandardShipment(string trackingCode, string description,
            decimal weight, decimal deliveryFee,
            DeliveryAddress destination)
            : base(trackingCode, description, weight, deliveryFee, destination)
        {
        }
    }

    #endregion

    #region ExpressShipment

    class ExpressShipment : Shipment
    {
        public decimal ExtraFee { get; set; }

        public ExpressShipment(string trackingCode, string description,
            decimal weight, decimal deliveryFee,
            DeliveryAddress destination, decimal extraFee)
            : base(trackingCode, description, weight, deliveryFee, destination)
        {
            ExtraFee = extraFee;
        }

        public override decimal EstimatedCost
        {
            get { return DeliveryFee + Weight * 5 + ExtraFee; }
        }

        public override void PrintShipment()
        {
            base.PrintShipment();
            Console.WriteLine($"Extra Fee     : {ExtraFee} EGP");
        }
    }

    #endregion

    #region InternationalShipment

    class InternationalShipment : Shipment
    {
        public string DestinationCountry { get; set; }
        public decimal CustomsFee { get; set; }

        public InternationalShipment(string trackingCode, string description,
            decimal weight, decimal deliveryFee,
            DeliveryAddress destination,
            string destinationCountry, decimal customsFee)
            : base(trackingCode, description, weight, deliveryFee, destination)
        {
            DestinationCountry = destinationCountry;
            CustomsFee = customsFee;
        }

        public override decimal EstimatedCost
        {
            get { return DeliveryFee + Weight * 5 + CustomsFee; }
        }

        public override void PrintShipment()
        {
            base.PrintShipment();
            Console.WriteLine($"Destination Country : {DestinationCountry}");
            Console.WriteLine($"Customs Fee         : {CustomsFee} EGP");
        }
    }

    #endregion

    #region DeliveryCenter

    class DeliveryCenter
    {
        public string CenterName { get; set; }

        private Shipment[] shipments = new Shipment[20];
        private int count = 0;

        public DeliveryCenter(string centerName)
        {
            CenterName = centerName;
        }

        public Shipment this[string trackingCode]
        {
            get
            {
                for (int i = 0; i < count; i++)
                {
                    if (shipments[i].TrackingCode == trackingCode)
                        return shipments[i];
                }

                return null;
            }
        }

        public void AddShipment(Shipment shipment)
        {
            if (count < 20)
            {
                shipments[count] = shipment;
                count++;
            }
        }

        public bool RemoveShipment(string trackingCode)
        {
            for (int i = 0; i < count; i++)
            {
                if (shipments[i].TrackingCode == trackingCode)
                {
                    for (int j = i; j < count - 1; j++)
                        shipments[j] = shipments[j + 1];

                    count--;
                    return true;
                }
            }

            return false;
        }

        public void PrintAllShipments()
        {
            for (int i = 0; i < count; i++)
            {
                shipments[i].PrintShipment();
                Console.WriteLine();
            }
        }
    }

    #endregion

    #region Program

    class Program
    {
        static void Main()
        {
            Console.Write("Enter Center Name: ");
            string name = Console.ReadLine();

            DeliveryCenter center = new DeliveryCenter(name);

            Console.WriteLine();
            Console.WriteLine("Enter Standard Shipment");

            Console.Write("Tracking Code: ");
            string trackingCode1 = Console.ReadLine();

            Console.Write("Description: ");
            string description1 = Console.ReadLine();

            Console.Write("Weight: ");
            decimal weight1 = decimal.Parse(Console.ReadLine());

            Console.Write("Delivery Fee: ");
            decimal fee1 = decimal.Parse(Console.ReadLine());

            Console.Write("City: ");
            string city1 = Console.ReadLine();

            Console.Write("Country: ");
            string country1 = Console.ReadLine();

            StandardShipment s1 = new StandardShipment(
                trackingCode1,
                description1,
                weight1,
                fee1,
                new DeliveryAddress(city1, country1));

            center.AddShipment(s1);


            Console.WriteLine();
            Console.WriteLine("Enter Express Shipment");

            Console.Write("Tracking Code: ");
            string trackingCode2 = Console.ReadLine();

            Console.Write("Description: ");
            string description2 = Console.ReadLine();

            Console.Write("Weight: ");
            decimal weight2 = decimal.Parse(Console.ReadLine());

            Console.Write("Delivery Fee: ");
            decimal fee2 = decimal.Parse(Console.ReadLine());

            Console.Write("City: ");
            string city2 = Console.ReadLine();

            Console.Write("Country: ");
            string country2 = Console.ReadLine();

            Console.Write("Extra Fee: ");
            decimal extraFee = decimal.Parse(Console.ReadLine());

            ExpressShipment s2 = new ExpressShipment(
                trackingCode2,
                description2,
                weight2,
                fee2,
                new DeliveryAddress(city2, country2),
                extraFee);

            center.AddShipment(s2);


            Console.WriteLine();
            Console.WriteLine("Enter International Shipment");

            Console.Write("Tracking Code: ");
            string trackingCode3 = Console.ReadLine();

            Console.Write("Description: ");
            string description3 = Console.ReadLine();

            Console.Write("Weight: ");
            decimal weight3 = decimal.Parse(Console.ReadLine());

            Console.Write("Delivery Fee: ");
            decimal fee3 = decimal.Parse(Console.ReadLine());

            Console.Write("City: ");
            string city3 = Console.ReadLine();

            Console.Write("Country: ");
            string country3 = Console.ReadLine();

            Console.Write("Destination Country: ");
            string destinationCountry = Console.ReadLine();

            Console.Write("Customs Fee: ");
            decimal customsFee = decimal.Parse(Console.ReadLine());

            InternationalShipment s3 = new InternationalShipment(
                trackingCode3,
                description3,
                weight3,
                fee3,
                new DeliveryAddress(city3, country3),
                destinationCountry,
                customsFee);

            center.AddShipment(s3);


            Console.WriteLine();
            Console.WriteLine("All Shipments");
            center.PrintAllShipments();

            Console.Write("Enter Tracking Code to Search: ");
            string search = Console.ReadLine();

            Shipment shipment = center[search];

            if (shipment != null)
                shipment.PrintShipment();


            Console.Write("Enter Tracking Code to Remove: ");
            string remove = Console.ReadLine();

            if (center.RemoveShipment(remove))
                Console.WriteLine("Shipment Removed Successfully.");

            Console.WriteLine();
            Console.WriteLine("Remaining Shipments");
            center.PrintAllShipments();
        }
    }

#endregion

}