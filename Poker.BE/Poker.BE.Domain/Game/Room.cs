using Poker.BE.Domain.Utility.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Poker.BE.Domain.Game
{

    /// <summary> Defined the room that the players are playing at the game </summary>
    /// <remarks>
    /// <author>Idan Izicovich</author>
    /// <lastModified>2017-04-25</lastModified>
    /// </remarks>
    public class Room
    {
        #region Fields
        private ICollection<Player> activeAndPassivePlayers;
        private Deck deck;
        private Chair[] chairs;
        private int dealerIndex = 0;
        #endregion

        #region Properties
        // TODO: do we need ID for the Room? if so, what type should it be? 'long?' means nullable long.
        //public long? ID { get; }

        public ICollection<Chair> Chairs { get { return chairs; } }
        public Hand CurrentHand { get; private set; }
        public GamePreferences Preferences { get; set; }
        private ICollection<Player> ActivePlayers
        {
            get
            {
                return activeAndPassivePlayers.Where(
                    player => (player.CurrentState == Player.State.ActiveUnfolded | player.CurrentState == Player.State.ActiveFolded))
                    .ToList();
            }
        }
        private ICollection<Player> PassivePlayers
        {
            get
            {
                return activeAndPassivePlayers.Where(
                    player => (player.CurrentState == Player.State.Passive))
                    .ToList();
            }
        }

        
        #endregion

        #region Constructors
        private Room()
        {
            activeAndPassivePlayers = new List<Player>();
            deck = new Deck();
            chairs = new Chair[Chair.NCHAIRS_IN_ROOM];

            for (int i = 0; i < Chair.NCHAIRS_IN_ROOM; i++)
            {
                Chairs.ToArray()[i] = new Chair(i);
            }

            CurrentHand = null;

            // Note: making default preferences to the room (poker game)
            Preferences = new GamePreferences();

        }

        /// <summary>
        /// UC003 Create a new room 
        /// </summary>
        /// <param name="creator">enter the room as a passive player.</param>
        /// <see cref="https://docs.google.com/document/d/1ob4bSynssE3UOfehUAFNv_VDpPbybhS4dW_O-v-QDiw/edit#heading=h.tzy1eb1jifgr"/>
        public Room(Player creator) : this()
        {
            activeAndPassivePlayers.Add(creator); DataMisalignedException;
        }

        /// <summary>
        /// UC003 Create a new room 
        /// </summary>
        /// <param name="creator">enter the room as a passive player.</param>
        /// <param name="preferences">limit / no limit / pot limit </param>
        /// <see cref="https://docs.google.com/document/d/1ob4bSynssE3UOfehUAFNv_VDpPbybhS4dW_O-v-QDiw/edit#heading=h.tzy1eb1jifgr"/>
        public Room(Player creator, GamePreferences preferences) : this(creator)
        {
            Preferences = preferences;
        }

        #endregion

        #region Methods
        public Player CreatePlayer()
        {
            var result = new Player();
            activeAndPassivePlayers.Add(result);
            return result;
        }

        /// <summary>
        /// UC014 Start (Deal) a New Hand
        /// </summary>
        /// <see cref="https://docs.google.com/document/d/1OTee6BGDWK2usL53jdoeBOI-1Jh8wyNejbQ0ZroUhcA/edit#heading=h.3z6a7b6nlnjj"/>
        public void StartNewHand()
        {
            if (ActivePlayers.Count < 2)
            {
                throw new NotEnoughPlayersException("Its should be at least 2 active players to start new hand!");
            }
            if (deck.Cards.Count != Deck.NCARDS)
            {
                throw new NotEnoughPlayersException("Cards must be dealt from a proper deck (standard 52-card deck containing no jokers)");
            }
            if (this.CurrentHand != null && this.CurrentHand.Active)
            {
                throw new NotEnoughPlayersException("The previous hand hasnt ended");
            }
            deck.ShuffleCards();
            Player dealer = ActivePlayers.ElementAt(dealerIndex);
            CurrentHand = new Hand(dealer, deck, ActivePlayers);
            CurrentHand.DealCards();
            CurrentHand.PlaceBlinds(Preferences);
            //TODO: Check If HEAD-TO-HEAD / HEADS UP alternative flow workds here.

        }
        public void EndCurrentHand()
        {
            CurrentHand.endHand();
            dealerIndex++;
            //TODO: implementation
        }

        /// <summary>
        /// UC027 Choose Play Move
        /// </summary>
        /// <see cref="https://docs.google.com/document/d/1OTee6BGDWK2usL53jdoeBOI-1Jh8wyNejbQ0ZroUhcA/edit#heading=h.8f3okxza6g2d"/>
        public void ChoosePlayMove(Round.Move move)
        {
            if (ActivePlayers.Where(player => player.CurrentState == Player.State.ActiveUnfolded).ToList().Count < 2)
            {
                throw new NotEnoughPlayersException("Its should be at least 2 active players to play move");
            }
            CurrentHand.CurrentRound.PlayMove(move);
            
        }

        // TODO: Take a chair, leave a chair - will chairsSemaphore.WaitOne() or Release()

        public void SendMessage()
        {
            //TODO: 'UC006: Send Message to Room’s Chat' - for the ones that doing that
        }
        #endregion

        #region Private Functions
        private void TakeAChair(int index)
        {
            chairs[index].Take();
        }

        private void ReleaseAChair(int index)
        {
            chairs[index].Release();
        }
        #endregion


    }//class
}
