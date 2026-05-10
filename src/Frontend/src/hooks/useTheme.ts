import React from 'react';
import { useAppStore } from '@stores/appStore';

export function useTheme() {
  const { isDarkMode, toggleTheme } = useAppStore();

  React.useEffect(() => {
    if (isDarkMode) {
      document.documentElement.classList.add('dark');
    } else {
      document.documentElement.classList.remove('dark');
    }
  }, [isDarkMode]);

  return { isDarkMode, toggleTheme };
}
