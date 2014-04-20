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

    public class PermissionState
    {
        private String name;

        public static readonly PermissionState REQUESTED = new PermissionState("REQUESTED");
        public static readonly PermissionState CLAIMED = new PermissionState("CLAIMED");
        public static readonly PermissionState GRANTED = new PermissionState("GRANTED");
        public static readonly PermissionState DENIED = new PermissionState("DENIED");
        public static readonly PermissionState UNIX_REQUESTED = new PermissionState("UNIX_REQUESTED");
        public static readonly PermissionState UNIX_CLAIMED = new PermissionState("UNIX_CLAIMED");

        private PermissionState(String name)
        {
            this.name = name;
        }

        public String toString()
        {
            return name;
        }
    }

    public class SystemPermission
    {
        private SystemProfile profile;
        private SystemUser requestor;
        private SystemAdmin admin;
        private Boolean isGranted;
        private PermissionState permission_state;
        private Boolean isUnixPermissionGranted;

        public Boolean IsUnixPermissionGranted { get { return isUnixPermissionGranted; } }

        public PermissionState PermissionState { get { return permission_state; } }
        public Boolean IsGranted { get { return isGranted; } }
        public SystemAdmin Admin { set { admin = value; } get { return admin; } }

        public SystemPermission(SystemUser requestor, SystemProfile profile)
        {
            this.requestor = requestor;
            this.profile = profile;
            setPermissionState(PermissionState.REQUESTED);
            isGranted = false;
            isUnixPermissionGranted = false;
            notifyAdminOfPermissionRequest();
        }

        private void setPermissionState(PermissionState state)
        {
            permission_state = state;
        }

        public void claimedBy(SystemAdmin admin)
        {
            if (!permission_state.Equals(PermissionState.REQUESTED) && !permission_state.Equals(PermissionState.UNIX_REQUESTED))
                return;
            willBeHandledBy(admin);
            if (permission_state.Equals(PermissionState.REQUESTED))
                setPermissionState(PermissionState.CLAIMED);
            else if (permission_state.Equals(PermissionState.UNIX_REQUESTED))
                setPermissionState(PermissionState.UNIX_CLAIMED);
        }

        public void deniedBy(SystemAdmin admin)
        {
            if (!permission_state.Equals(PermissionState.CLAIMED) && !permission_state.Equals(PermissionState.UNIX_CLAIMED))
                return;
            if (!this.admin.Equals(admin))
                return;
            isGranted = false;
            isUnixPermissionGranted = false;
            setPermissionState(PermissionState.DENIED);
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
                setPermissionState(PermissionState.UNIX_REQUESTED);
                notifyUnixAdminsOfPermissionRequest();
                return;
            }
            setPermissionState(PermissionState.GRANTED);
            isGranted = true;
            notifyUserOfPermissionRequestResult();
        }

        private bool isInClaimedState()
        {
            return permission_state.Equals(PermissionState.CLAIMED) || permission_state.Equals(PermissionState.UNIX_CLAIMED);
        }

        private bool isUnixPermissionRequestedAndClaimed()
        {
            return profile.IsUnixPermissionRequired && permission_state.Equals(PermissionState.UNIX_CLAIMED);
        }

        void notifyAdminOfPermissionRequest() { }
        void notifyUserOfPermissionRequestResult() { }
        void willBeHandledBy(SystemAdmin admin) { }

        void notifyUnixAdminsOfPermissionRequest() { }
    }
}
