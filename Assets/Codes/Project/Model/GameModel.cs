using QFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformShoot
{
    public interface IGameModel : IModel
    {
        BindableProperty<int> Score
        {
            get;
        }
    }
    public class GameModel : AbstractModel, IGameModel
    {
        protected override void OnInit()
        {
            
        }
        BindableProperty<int> IGameModel.Score { get; } = new BindableProperty<int>(0);
    }
}