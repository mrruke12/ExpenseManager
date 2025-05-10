import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import tailwindcss from '@tailwindcss/vite'
// import basicSsl from '@vitejs/plugin-basic-ssl'

// https://vite.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    // basicSsl(),
    tailwindcss()
  ],
  // server: {
  //   https: true,
  //   host: '0.0.0.0'
  // }
})
