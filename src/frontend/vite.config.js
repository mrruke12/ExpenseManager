import { defineConfig, loadEnv } from 'vite'
import vue from '@vitejs/plugin-vue'
import tailwindcss from '@tailwindcss/vite'
import basicSsl from '@vitejs/plugin-basic-ssl'

// https://vite.dev/config/
export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd(), '');

  return {
      plugins: [
      vue(),
      // basicSsl(),
      tailwindcss()
    ],
    server: {
      https: false,
      host: env.VITE_FRONTEND_HOST,
      port: env.VITE_FRONTEND_PORT
    }
  }
})
