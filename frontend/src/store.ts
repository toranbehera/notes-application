import { defineStore } from "pinia"

export const useThemeStore = defineStore('theme', {
  state: () => {
    const isDark = false;
    
    return { isDark }
  },

  actions: {
    toggleTheme() {
      this.isDark = !this.isDark;
    }
  }
})
