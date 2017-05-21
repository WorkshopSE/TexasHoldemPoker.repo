using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.BE.Service.Modules.Requests;
using Poker.BE.Service.Modules.Results;
using Poker.BE.Domain.Game;
using Poker.BE.Domain.Utility.Exceptions;
using Poker.BE.Domain.Core;
using Poker.BE.Domain.Utility.Logger;

namespace Poker.BE.Service.Services
{
    public class PokerGamePlayService : IServices.IPokerGamePlayService
    {
        #region Properties
        public Round round { get; set; }
        public Hand hand { get; set; }
        #endregion

        public ChoosePlayMoveResult ChoosePlayMove(ChoosePlayMoveRequest request)
        {
            var result = new ChoosePlayMoveResult();
            try
            {
                result.TotalRaise = round.PlayMove(request.playMove, request.amountToBetOrCall);
            }
            catch (GameRulesException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            catch (WrongIOException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            catch (NotEnoughMoneyException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            catch (NotEnoughPlayersException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            return result;
        }

        public StartNewHandResult StartNewHand(StartNewHandRequest request)
        {
            //-----------TODO - fix after merge-----------//

            var result = new StartNewHandResult();
            try
            {
                //result.Hand = hand.StartNewHand();
            }
            catch (GameRulesException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            catch (WrongIOException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            catch (NotEnoughMoneyException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            catch (NotEnoughPlayersException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            return result;
        }
    }
}
