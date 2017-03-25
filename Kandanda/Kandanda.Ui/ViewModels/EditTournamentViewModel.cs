﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Kandanda.Ui.Core;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;

namespace Kandanda.Ui.ViewModels
{
    public class EditTournamentViewModel : ViewModelBase, IConfirmNavigationRequest
    {
        public InteractionRequest<IConfirmation> ConfirmationRequest { get; }

        public EditTournamentViewModel()
        {
            ConfirmationRequest = new InteractionRequest<IConfirmation>();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public async void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            var confirmation = await ConfirmationRequest.RaiseAsync(
                new Confirmation {Title = "Kandanda", Content = "Are you sure you want to close this Tournament?"});
            continuationCallback(confirmation.Confirmed);
        }
    }
}
