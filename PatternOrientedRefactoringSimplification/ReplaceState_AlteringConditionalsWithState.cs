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

        public virtual void claimedBy(SystemAdmin admin, SystemPermission system_permission) { }

        public virtual void deniedBy(SystemAdmin admin, SystemPermission system_permission){}

        public virtual void grantedBy(SystemAdmin admin, SystemPermission system_permission){}

        protected void willBeHandledBy(SystemAdmin admin) { }
        protected void notifyUserOfPermissionRequestResult() { }
        protected void notifyUnixAdminsOfPermissionRequest() { }
    }

    class PermissionRrequest:PermissionState
    {
        public PermissionRrequest() : base("REQUESTED") { }

        public override void claimedBy(SystemAdmin admin, SystemPermission system_permission)
        {
            willBeHandledBy(admin);
            system_permission.setPermissionState(PermissionState.CLAIMED);
        }

    }
    class PermissionClaimed : PermissionState
    {
        public PermissionClaimed() : base("CLAIMED") { }
        public override void grantedBy(SystemAdmin admin, SystemPermission system_permission)
        {

            system_permission.setPermissionState(PermissionState.UNIX_REQUESTED);
            notifyUnixAdminsOfPermissionRequest();
        }
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
        public override void claimedBy(SystemAdmin admin, SystemPermission system_permission)
        {
            willBeHandledBy(admin);
            system_permission.setPermissionState(PermissionState.UNIX_CLAIMED);
        }

    }
    class PermissionUnix_Claimed : PermissionState
    {
        public PermissionUnix_Claimed() : base("UNIX_CLAIMED") { }
        public override void deniedBy(SystemAdmin admin, SystemPermission system_permission)
        {
            system_permission.IsGranted = false;
            system_permission.IsUnixPermissionGranted = false;
            system_permission.setPermissionState(PermissionState.DENIED);
            notifyUserOfPermissionRequestResult();
        }
        public override void grantedBy(SystemAdmin admin, SystemPermission system_permission)
        {
            if (!admin.Equals(admin))
                return;

            if (system_permission.Profile.IsUnixPermissionRequired)
                system_permission.IsUnixPermissionGranted = true;
            system_permission.setPermissionState(PermissionState.GRANTED);
            system_permission.IsGranted = true;
            notifyUserOfPermissionRequestResult();
        }
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
