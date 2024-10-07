using Microsoft.AspNetCore.SignalR;
using SignalR.Services;

namespace SignalR.Hubs
{
    public class PizzaHub : Hub
    {
        private readonly PizzaManager _pizzaManager;

        public string groupName;
        public int nbUsers;
        public int Money;
        public int NbPizza;

        public PizzaHub(PizzaManager pizzaManager) {
            _pizzaManager = pizzaManager;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            _pizzaManager.AddUser();
            nbUsers = _pizzaManager.NbConnectedUsers;
            await Clients.All.SendAsync(nbUsers.ToString());
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnConnectedAsync();
            _pizzaManager.RemoveUser();
            nbUsers = _pizzaManager.NbConnectedUsers;
            await Clients.All.SendAsync(nbUsers.ToString());
        }

        public async Task SelectChoice(PizzaChoice choice)
        {
            groupName = _pizzaManager.GetGroupName(choice);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task UnselectChoice(PizzaChoice choice)
        {
            groupName = _pizzaManager.GetGroupName(choice);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task AddMoney(PizzaChoice choice)
        {
            groupName = _pizzaManager.GetGroupName(choice);
            _pizzaManager.IncreaseMoney(choice);
            await Clients.Group(groupName).SendAsync(_pizzaManager.NbPizzas.ToString());
        }

        public async Task BuyPizza(PizzaChoice choice)
        {
            groupName = _pizzaManager.GetGroupName(choice);
            _pizzaManager.BuyPizza(choice);
            await Clients.Group(groupName).SendAsync(_pizzaManager.Money.ToString());
        }
    }
}
