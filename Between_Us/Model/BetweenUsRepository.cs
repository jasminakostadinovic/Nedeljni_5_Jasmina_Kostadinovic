using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Between_Us.Model
{
    class BetweenUsRepository
    {
        internal int GetUserDataId(string userName)
        {
            try
            {
                using (var conn = new BetweenUsEntities())
                {
                    var user = conn.tblUsers.FirstOrDefault(x => x.Username == userName);
                    if (user != null)
                        return user.UserID;
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        internal bool TryAddNewUser(tblUser user)
        {
            try
            {
                using (var conn = new BetweenUsEntities())
                {
                    conn.tblUsers.Add(user);
                    conn.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal List<tblUser> LoadUsers(int userId)
        {
            try
            {
                using (var conn = new BetweenUsEntities())
                {
                    if (conn.tblUsers.Any())
                        return conn.tblUsers.Where(x => x.UserID != userId).Include(y => y.tblFriendRequests).ToList();
                    return new List<tblUser>();
                }
            }
            catch (Exception)
            {
                return new List<tblUser>();
            }
        }

        internal List<tblFriendRequest> LoadAllRequests(int userId)
        {
            try
            {
                using (var conn = new BetweenUsEntities())
                {
                    if (conn.tblFriendRequests.Any())
                        return conn.tblFriendRequests
                            .Where(x => x.tblUser1.UserID == userId 
                            || x.tblUser.UserID == userId).ToList();
                    return new List<tblFriendRequest>();
                }
            }
            catch (Exception)
            {
                return new List<tblFriendRequest>();
            }
        }

        internal List<tblFriendRequest> LoadRequestsForUser(int userId)
        {
            try
            {
                using (var conn = new BetweenUsEntities())
                {
                    if (conn.tblFriendRequests.Any())
                        return conn.tblFriendRequests.Where(x => x.tblUser1.UserID == userId).ToList();
                    return new List<tblFriendRequest>();
                }
            }
            catch (Exception)
            {
                return new List<tblFriendRequest>();
            }
        }

        internal List<tblPost> LoadPosts()
        {
            try
            {
                using (var conn = new BetweenUsEntities())
                {
                    if (conn.tblPosts.Any())
                        return conn.tblPosts.Include(x => x.tblUser).ToList();
                    return new List<tblPost>();
                }
            }
            catch (Exception)
            {
                return new List<tblPost>();
            }
        }

        internal bool SendRequest(int user1id, int user2id)
        {
            try
            {
                using (var conn = new BetweenUsEntities())
                {
                    conn.tblFriendRequests.Add(new tblFriendRequest() { UserID = user1id, UserID2 = user2id });
                    conn.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal bool AddNewFriend(int user1id, int user2id)
        {
            try
            {
                using (var conn = new BetweenUsEntities())
                {
                    conn.tblFriends.Add(new tblFriend() { UserID = user1id, UserID2 = user2id });
                    conn.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal tblUser LoadUser(int userId)
        {
            try
            {
                using (var conn = new BetweenUsEntities())
                {
                    return conn.tblUsers.FirstOrDefault(x => x.UserID == userId);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal tblUser LoadUsers(object userId)
        {
            throw new NotImplementedException();
        }
    }
}
