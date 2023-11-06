using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using BlApi;
using BlImplementation;
using BO;


namespace SimulatorLib
{
    public static class Simulator
    {
        public delegate void StatusChanged(BO.Order order, eCondition newStatus, DateTime prev, DateTime next);
        public static event StatusChanged? StatusChangedEvent = null;
        public delegate void FinishSimulator(DateTime end);
        public static event FinishSimulator? FinishSimulatorEvent = null;
        volatile private static bool stopRequest = false;
        public static bool IsAlive { get; set; } = false;
        public static void startSimulator()
        {
            IsAlive = true;
            Thread thread = new Thread(Run);
            stopRequest = false;
            thread.Start();
        }
        public static void Run()
        {
            IBL bl = BlApi.Factory.Get();
            Random random = new Random();

           

            while (true)
            {
                int? currentOrderId = bl?.Order.getOrderToUpdate();
                if (currentOrderId == 0)
                {
                    break;
                }
                BO.Order? orderToTreat = bl?.Order.GetOrdersDetails(Convert.ToInt32(currentOrderId));
               
                int CurrentHandleTime = random.Next(1000, 5000);
                BO.eCondition? prevStatus = orderToTreat?.orderCondition;
                DateTime startOfChange = DateTime.Now;
                Thread.Sleep(CurrentHandleTime);
                if (orderToTreat?.orderCondition == BO.eCondition.OrderSent)
                {
                    bl?.Order.UpdateDelivered(Convert.ToInt32(currentOrderId));
                    orderToTreat.orderCondition = eCondition.OrderSupllied;
                }
                else
                {
                    bl?.Order.UpdateSent(Convert.ToInt32(currentOrderId));
                    orderToTreat.orderCondition = eCondition.OrderSent;
                }
                DateTime endOfChange = DateTime.Now;
                StatusChangedEvent?.Invoke(orderToTreat ?? throw new Exception(), orderToTreat.orderCondition, startOfChange, endOfChange);
                Thread.Sleep(1000);
            }
            FinishSimulatorEvent?.Invoke(DateTime.Now);

        }

        public static void Stop()
        {
            stopRequest = true;
            IsAlive = false;
        }
    }
}

