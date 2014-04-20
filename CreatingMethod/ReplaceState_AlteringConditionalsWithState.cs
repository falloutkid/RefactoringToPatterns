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

    #region Permission State一覧
    public abstract class PermissionState
    {
        protected String name;

        public static readonly PermissionState REQUESTED = new PermissionRrequest();
        public static readonly PermissionState CLAIMED = new PermissionClaimed();
        public static readonly PermissionState GRANTED = new PermissionGranted();
        public static readonly PermissionState DENIED = new PermissionDenied();
        public static readonly PermissionState UNIX_REQUESTED = new PermissionUnix_Requested();
        public static readonly PermissionState UNIX_CLAIMED = new PermissionUnix_Claimed();

        protected PermissionState(String name)
        {
            this.name = name;
        }

        public String toString()
        {
            return name;
        }

        public void claimedBy(SystemAdmin admin, SystemPermission system_permission)
        {
            if (!system_permission.PermissionState.Equals(PermissionState.REQUESTED) && !system_permission.PermissionState.Equals(PermissionState.UNIX_REQUESTED))
                return;
            willBeHandledBy(admin);
            if (system_permission.PermissionState.Equals(PermissionState.REQUESTED))
                system_permission.setPermissionState(PermissionState.CLAIMED);
            else if (system_permission.PermissionState.Equals(PermissionState.UNIX_REQUESTED))
                system_permission.setPermissionState(PermissionState.UNIX_CLAIMED);
        }

        public void deniedBy(SystemAdmin admin, SystemPermission system_permission)
        {
            if (!system_permission.PermissionState.Equals(PermissionState.CLAIMED) && !system_permission.PermissionState.Equals(PermissionState.UNIX_CLAIMED))
                return;
            if (!admin.Equals(admin))
                return;
            system_permission.IsGranted = false;
            system_permission.IsUnixPermissionGranted = false;
            system_permission.setPermissionState(PermissionState.DENIED);
            notifyUserOfPermissionRequestResult();
        }

        public void grantedBy(SystemAdmin admin, SystemPermission system_permission)
        {
            if (!system_permission.isInClaimedState())
                return;
            if (!admin.Equals(admin))
                return;

            if (system_permission.isUnixPermissionRequestedAndClaimed())
                system_permission.IsUnixPermissionGranted = true;
            else if (system_permission.Profile.IsUnixPermissionRequired && !system_permission.IsUnixPermissionGranted)
            {
                system_permission.setPermissionState(PermissionState.UNIX_REQUESTED);
                notifyUnixAdminsOfPermissionRequest();
                return;
            }
            system_permission.setPermissionState(PermissionState.GRANTED);
            system_permission.IsGranted = true;
            notifyUserOfPermissionRequestResult();
        }

        void willBeHandledBy(SystemAdmin admin) { }
        void notifyUserOfPermissionRequestResult() { }
        void notifyUnixAdminsOfPermissionRequest() { }
    }

    class PermissionRrequest:PermissionState
    {
        public PermissionRrequest() : base("REQUESTED") { }
    }
    class PermissionClaimed : PermissionState
    {
        public PermissionClaimed() : base("CLAIMED") { }
    }
    class PermissionGranted : PermissionState
    {
        public PermissionGranted() : base("GRANTED") { }
    }
    class PermissionDenied : PermissionState
    {
        public PermissionDenied() : base("DENIED") { }
    }
    class PermissionUnix_Requested : PermissionState
    {
        public PermissionUnix_Requested() : base("UNIX_REQUESTED") { }
    }
    class PermissionUnix_Claimed : PermissionState
    {
        public PermissionUnix_Claimed() : base("UNIX_CLAIMED") { }
    }
    #endregion

    public class SystemPermission
    {
        private SystemProfile profile;
        private SystemUser requestor;
        private SystemAdmin admin;
        
        private PermissionState permission_state;

        private Boolean isGranted;
        public Boolean IsGranted
        {
            get { return isGranted; }
            set { isGranted = value; }
        }
        private Boolean isUnixPermissionGranted;
        public Boolean IsUnixPermissionGranted
        {
            get { return isUnixPermissionGranted; }
            set { isUnixPermissionGranted = value; }
        }

        public PermissionState PermissionState { get { return permission_state; } }
        public SystemAdmin Admin { set { admin = value; } get { return admin; } }
        public SystemProfile Profile { get { return profile; } }

        public SystemPermission(SystemUser requestor, SystemProfile profile)
        {
            this.requestor = requestor;
            this.profile = profile;
            setPermissionState(PermissionState.REQUESTED);
            isGranted = false;
            isUnixPermissionGranted = false;
            notifyAdminOfPermissionRequest();
        }

        public void setPermissionState(PermissionState state)
        {
            permission_state = state;
        }

        public void claimedBy(SystemAdmin admin)
        {
            PermissionState.claimedBy(admin, this);
        }

        public void deniedBy(SystemAdmin admin)
        {
            PermissionState.deniedBy(admin, this);
        }

        public void grantedBy(SystemAdmin admin)
        {
            PermissionState.grantedBy(admin, this);
        }

        public bool isInClaimedState()
        {
            return permission_state.Equals(PermissionState.CLAIMED) || permission_state.Equals(PermissionState.UNIX_CLAIMED);
        }

        public bool isUnixPermissionRequestedAndClaimed()
        {
            return profile.IsUnixPermissionRequired && permission_state.Equals(PermissionState.UNIX_CLAIMED);
        }

        void notifyAdminOfPermissionRequest() { }
    }
}
