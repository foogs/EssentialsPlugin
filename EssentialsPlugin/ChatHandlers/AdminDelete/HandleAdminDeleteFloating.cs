﻿namespace EssentialsPlugin.ChatHandlers
{
    using System.Collections.Generic;
    using System.Linq;
    using EssentialsPlugin.Utility;
    using Sandbox.Engine.Multiplayer;
    using Sandbox.Game.Entities;
    using Sandbox.Game.Replication;
    using Sandbox.ModAPI;
    using SEModAPIInternal.API.Common;
    using SEModAPIInternal.API.Entity.Sector.SectorObject;
    using SEModAPIInternal.API.Entity.Sector.SectorObject.CubeGrid;
    using SEModAPIInternal.API.Entity.Sector.SectorObject.CubeGrid.CubeBlock;
    using VRage.Game.Entity;
    using VRage.Game.ModAPI;
    using VRage.ModAPI;
    public class HandleAdminDeleteFloating : ChatHandlerBase
    {
        public override string GetHelp( )
        {
            return "This command will delete all floating objects in the world, including ore, components, and backpacks. Usage: /admin delete floating";
        }
        public override string GetCommandText( )
        {
            return "/admin delete floating";
        }

        public override Communication.ServerDialogItem GetHelpDialog( )
        {
            Communication.ServerDialogItem DialogItem = new Communication.ServerDialogItem( );
            DialogItem.title = "Help";
            DialogItem.header = "";
            DialogItem.content = GetHelp( );
            DialogItem.buttonText = "close";
            return DialogItem;
        }

        public override bool IsAdminCommand( )
        {
            return true;
        }

        public override bool AllowedInConsole( )
        {
            return true;
        }
                
        public override bool HandleCommand( ulong userId, string[ ] words )
        {
            int count = 0;
            HashSet<MyEntity> entities = new HashSet<MyEntity>( );

            Wrapper.GameAction( ( ) => entities = MyEntities.GetEntities(  ) );

            foreach ( MyEntity entity in entities )
            {
                if ( entity == null )
                    continue;

                if ( entity is MyFloatingObject || entity is MyInventoryBagEntity || entity is MyMeteor )
                {
                    count++;
                    Wrapper.BeginGameAction( entity.Close, null, null );
                }
            }
            Communication.SendPrivateInformation( userId, count.ToString( ) + " floating objects deleted." );
            return true;
        }

    }
}
