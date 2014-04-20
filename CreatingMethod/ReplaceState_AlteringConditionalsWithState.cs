using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoringSimplification
{
    public class SystemAdmin { }
    public class SystemProfile
    {
        private bool isUnixPermissionRequired;
        public bool IsUnixPermissionRequired { get { return isUnixPermissionRequired; } }
        public SystemProfile()
        {
            isUnixPermissionRequired = false;
        }
    }
    public class SystemUser { }

    public class SystemPermission
    {
        private SystemProfile profile;
        private SystemUser requestor;
        private SystemAdmin admin;
        private Boolean isGranted;
        private String state;
        private Boolean isUnixPermissionGranted;

        public Boolean IsUnixPermissionGranted { get { return isUnixPermissionGranted; } }

        public readonly String REQUESTED = "REQUESTED";
        public readonly String CLAIMED = "CLAIMED";
        public readonly String GRANTED = "GRANTED";
        public readonly String DENIED = "DENIED";
        public readonly String UNIX_REQUESTED = "UNIX_REQUESTED";
        public readonly String UNIX_CLAIMED = "UNIX_CLAIMED";

        public string State { get { return state; } }
        public Boolean IsGranted { get { return isGranted; } }
        public SystemAdmin Admin { set { admin = value; } get { return admin; } }

        public SystemPermission(SystemUser requestor, SystemProfile profile)
        {
            this.requestor = requestor;
            this.profile = profile;
            state = REQUESTED;
            isGranted = false;
            isUnixPermissionGranted = false;
            notifyAdminOfPermissionRequest();
        }

        public void claimedBy(SystemAdmin admin)
        {
            if (!state.Equals(REQUESTED) && !state.Equals(UNIX_REQUESTED))
                return;
            willBeHandledBy(admin);
            if (state.Equals(REQUESTED))
                state = CLAIMED;
            else if (state.Equals(UNIX_REQUESTED))
                state = UNIX_CLAIMED;
        }

        public void deniedBy(SystemAdmin admin)
        {
            if (!state.Equals(CLAIMED) && !state.Equals(UNIX_CLAIMED))
                return;
            if (!this.admin.Equals(admin))
                return;
            isGranted = false;
            isUnixPermissionGranted = false;
            state = DENIED;
            notifyUserOfPermissionRequestResult();
        }

        public void grantedBy(SystemAdmin admin)
        {
            if (!isInClaimedState())
                return;
            if (!this.admin.Equals(admin))
                return;

            if (isUnixPermissionRequestedAndClaimed())
                isUnixPermissionGranted = true;
            else if (profile.IsUnixPermissionRequired && !IsUnixPermissionGranted)
            {
                state = UNIX_REQUESTED;
                notifyUnixAdminsOfPermissionRequest();
                return;
            }
            state = GRANTED;
            isGranted = true;
            notifyUserOfPermissionRequestResult();
        }

        private bool isInClaimedState()
        {
            return state.Equals(CLAIMED) || state.Equals(UNIX_CLAIMED);
        }

        private bool isUnixPermissionRequestedAndClaimed()
        {
            return profile.IsUnixPermissionRequired && state.Equals(UNIX_CLAIMED);
        }

        void notifyAdminOfPermissionRequest() { }
        void notifyUserOfPermissionRequestResult() { }
        void willBeHandledBy(SystemAdmin admin) { }

        void notifyUnixAdminsOfPermissionRequest() { }
    }
}
