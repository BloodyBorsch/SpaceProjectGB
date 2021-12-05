using UnityEngine;


namespace MaksK_SpaceGB
{
    public static class ResourceLoadHelper
    {
        private static string _pathToAverageSpaceShip = "Data/AverageShipData";

        public static ShipData Loader(UnitSwitcher unit)
        {
            switch (unit)
            {
                case UnitSwitcher.AverageSpaceShip:
                    return (ShipData)Resources.Load(_pathToAverageSpaceShip);
                default:
                    Debug.Log("Ошибка в Загрузчике ресурсов");
                    return null;
            }
        }
    }
}