using SimpleInjector;

namespace StringCalculator
{
    internal class StringCalculatorTestModule
    {
        public static Container Container;

        public static void Start()
        {
            Container = new Container();

            Container.Register<ICalculatorV1, Calculator>(Lifestyle.Singleton);
            Container.Register<INumberServiceV1, NumberService>(Lifestyle.Singleton);

            Container.Verify();
        }
    }
}
