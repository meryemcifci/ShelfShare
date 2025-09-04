<script setup lang="ts">
import { useAuthStore } from "@/stores/authStore"; 
import { ref } from "vue";
import { useRouter } from "vue-router";

const fullName = ref("");
const email = ref("");
const password = ref("");
const authStore = useAuthStore();
const router = useRouter();

const handleRegister = async () => {
  try {
    await authStore.register(fullName.value, email.value, password.value);
    alert("Kayıt başarılı! Giriş yapabilirsiniz.");
    router.push("/login");
  } catch (err) {
    alert("Kayıt başarısız!");
  }
};
</script>

<template>
  <div>
    <h2>Register</h2>
    <input v-model="fullName" placeholder="Ad Soyad" />
    <input v-model="email" placeholder="Email" />
    <input v-model="password" type="password" placeholder="Şifre" />
    <button @click="handleRegister">Kayıt Ol</button>
  </div>
</template>
