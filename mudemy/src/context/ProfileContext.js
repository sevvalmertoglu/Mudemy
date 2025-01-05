import { createContext, useState, useContext, useEffect } from 'react';
import ProfileService from '../services/ProfileService'; 

const ProfileContext = createContext();

export function useProfile() {
  const context = useContext(ProfileContext);
  return context;
}

export const ProfileProvider = ({ children }) => {
  const [profile, setProfile] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchProfile = async () => {
      try {
        const profileData = await ProfileService.getProfile();
        setProfile(profileData);
      } catch (error) {
        console.error("Profile data fetching failed", error);
        setProfile({ userName: '', email: '' });
      } finally {
        setLoading(false);
      }
    };

    fetchProfile();
  }, []);

  console.log('Profile:', profile);

  if (loading) {
    return <div>Loading...</div>; 
  }

  return (
    <ProfileContext.Provider value={profile}>
      {children}
    </ProfileContext.Provider>
  );
};
