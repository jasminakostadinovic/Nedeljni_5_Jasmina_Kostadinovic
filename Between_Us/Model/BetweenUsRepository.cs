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
                        return conn.tblUsers
                            .Where(x => x.UserID != userId)
                            .ToList();
                    return new List<tblUser>();
                }
            }
            catch (Exception)
            {
                return new List<tblUser>();
            }
        }

        internal List<tblFriend> LoadUserFriends(int userId)
        {
            try
            {
                using (var conn = new BetweenUsEntities())
                {
                    if (conn.tblFriends.Any())
                        return conn.tblFriends
                            .Where(x => x.UserID == userId
                            || x.UserID2 == userId).ToList();
                    return new List<tblFriend>();
                }
            }
            catch (Exception)
            {
                return new List<tblFriend>();
            }
        }

        internal List<tblFriendRequest> LoadUserRequests(int userId)
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
                        return conn.tblFriendRequests                           
                            .Include(y => y.tblUser)
                            .Where(x => x.UserID2 == userId)
                            .ToList();
                    return new List<tblFriendRequest>();
                }
            }
            catch (Exception)
            {
                return new List<tblFriendRequest>();
            }
        }

        internal bool RemoveFriendRequest(int friendRequestID)
        {
            try
            {
                using (var conn = new BetweenUsEntities())
                {
                    var requestToRemove = conn.tblFriendRequests
                        .FirstOrDefault(x => x.FriendRequestID == friendRequestID);
                    if(requestToRemove != null)
                    {
                        conn.tblFriendRequests.Remove(requestToRemove);
                        conn.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
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
