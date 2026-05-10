import { create } from 'zustand';
import { persist } from 'zustand/middleware';

interface SavedProperty {
  id: string;
  title: string;
  price: number;
  thumbnailUrl: string;
  city: string;
}

interface AppStore {
  // Theme
  isDarkMode: boolean;
  toggleTheme: () => void;

  // Saved Properties
  savedProperties: SavedProperty[];
  addSavedProperty: (property: SavedProperty) => void;
  removeSavedProperty: (propertyId: string) => void;
  isSaved: (propertyId: string) => boolean;

  // Recently Viewed
  recentlyViewed: SavedProperty[];
  addToRecentlyViewed: (property: SavedProperty) => void;
  clearRecentlyViewed: () => void;
}

type PersistedAppState = Partial<AppStore>;

export const useAppStore = create<AppStore>()(
  persist(
    (set, get) => ({
      // Theme
      isDarkMode: window.matchMedia('(prefers-color-scheme: dark)').matches,
      toggleTheme: () => {
        set((state) => {
          const newMode = !state.isDarkMode;
          if (newMode) {
            document.documentElement.classList.add('dark');
          } else {
            document.documentElement.classList.remove('dark');
          }
          return { isDarkMode: newMode };
        });
      },

      // Saved Properties
      savedProperties: [],
      addSavedProperty: (property) => {
        set((state) => ({
          savedProperties: [...state.savedProperties, property],
        }));
      },
      removeSavedProperty: (propertyId) => {
        set((state) => ({
          savedProperties: state.savedProperties.filter((p) => p.id !== propertyId),
        }));
      },
      isSaved: (propertyId) => {
        const state = get();
        return state.savedProperties.some((p) => p.id === propertyId);
      },

      // Recently Viewed
      recentlyViewed: [],
      addToRecentlyViewed: (property) => {
        set((state) => {
          const filtered = state.recentlyViewed.filter((p) => p.id !== property.id);
          return {
            recentlyViewed: [property, ...filtered].slice(0, 10),
          };
        });
      },
      clearRecentlyViewed: () => {
        set({ recentlyViewed: [] });
      },
    }),
    {
      name: 'whatsreal-app-store',
      version: 1,
      migrate: (persistedState: unknown) => {
        const state = persistedState as PersistedAppState;
        if (!state.isDarkMode && window.matchMedia('(prefers-color-scheme: dark)').matches) {
          document.documentElement.classList.add('dark');
        }
        return state as AppStore;
      },
    },
  ),
);
