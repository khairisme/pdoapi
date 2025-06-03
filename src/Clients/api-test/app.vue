<template>
  <div class="container mt-5">
    <h1>API Test</h1>
    
    <!-- Religion Section -->
    <div class="section mt-4">
      <h2>Religion Selection</h2>
      <div class="form-group mt-3">
        <label for="religionSelect">Select Religion:</label>
        <select id="religionSelect" v-model="selectedValues.religion" class="form-control mt-2">
          <option value="" disabled>Please select a religion</option>
          <option v-for="religion in apiData.religions" :key="religion.code" :value="religion.code">
            {{ religion.name }}
          </option>
        </select>
      </div>
      <div class="mt-3" v-if="selectedValues.religion">
        <h3>Selected Religion Details:</h3>
        <div v-if="selectedReligionDetails">
          <p><strong>Code:</strong> {{ selectedReligionDetails.code }}</p>
          <p><strong>Name:</strong> {{ selectedReligionDetails.name }}</p>
          <p><strong>Description:</strong> {{ selectedReligionDetails.description }}</p>
          <p><strong>Active:</strong> {{ selectedReligionDetails.isActive ? 'Yes' : 'No' }}</p>
        </div>
      </div>
      <div v-if="errors.religion" class="alert alert-danger mt-3">
        {{ errors.religion }}
      </div>
    </div>
    
    <!-- Rank Section -->
    <div class="section mt-5">
      <h2>Rank Selection</h2>
      <div class="form-group mt-3">
        <label for="rankSelect">Select Rank:</label>
        <select id="rankSelect" v-model="selectedValues.rank" class="form-control mt-2">
          <option value="" disabled>Please select a rank</option>
          <option v-for="rank in apiData.ranks" :key="rank.code" :value="rank.code">
            {{ rank.name }}
          </option>
        </select>
      </div>
      <div class="mt-3" v-if="selectedValues.rank">
        <h3>Selected Rank Details:</h3>
        <div v-if="selectedRankDetails">
          <p><strong>Code:</strong> {{ selectedRankDetails.code }}</p>
          <p><strong>Name:</strong> {{ selectedRankDetails.name }}</p>
          <p><strong>Description:</strong> {{ selectedRankDetails.description }}</p>
          <p><strong>Active:</strong> {{ selectedRankDetails.isActive ? 'Yes' : 'No' }}</p>
        </div>
      </div>
      <div v-if="errors.rank" class="alert alert-danger mt-3">
        {{ errors.rank }}
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue';

// API data state variables
const apiData = ref({
  religions: [],
  ranks: []
});
const selectedValues = ref({
  religion: '',
  rank: ''
});
const loading = ref(false);
const errors = ref({
  religion: null,
  rank: null
});

// Computed properties for selected items
const selectedReligionDetails = computed(() => {
  if (!selectedValues.value.religion) return null;
  return apiData.value.religions.find(religion => religion.code === selectedValues.value.religion);
});

const selectedRankDetails = computed(() => {
  if (!selectedValues.value.rank) return null;
  return apiData.value.ranks.find(rank => rank.code === selectedValues.value.rank);
});

// Fetch all API data in a single function
async function fetchAllApiData() {
  loading.value = true;
  errors.value = { religion: null, rank: null };
  
  // Define API endpoints
  const endpoints = {
    religions: 'https://ppahrm.cogent.space/api/BloodGroups',
    ranks: 'https://ppahrm.cogent.space/api/Rank'
  };
  
  // Create an array of promises for all API calls
  const apiPromises = Object.entries(endpoints).map(async ([key, url]) => {
    try {
      const response = await fetch(url, {
        method: 'GET',
        headers: {
          'accept': 'application/json'
        }
      });
      
      if (!response.ok) {
        throw new Error(`Error: ${response.status} - ${response.statusText}`);
      }
      
      const data = await response.json();
      return { key, data, error: null };
    } catch (err) {
      console.error(`Failed to fetch ${key}:`, err);
      return { key, data: [], error: `Failed to load ${key}: ${err.message}` };
    }
  });
  
  // Wait for all promises to resolve
  const results = await Promise.allSettled(apiPromises);
  
  // Process results
  results.forEach(result => {
    if (result.status === 'fulfilled') {
      const { key, data, error } = result.value;
      apiData.value[key] = data;
      if (error) errors.value[key] = error;
    }
  });
  
  loading.value = false;
}

// Load all API data when component mounts
onMounted(() => {
  fetchAllApiData();
});
</script>

<style scoped>
.container {
  max-width: 800px;
  margin: 0 auto;
}

.section {
  padding: 20px;
  border: 1px solid #e9ecef;
  border-radius: 8px;
  background-color: #f8f9fa;
}

select {
  width: 100%;
  padding: 8px;
  border-radius: 4px;
  border: 1px solid #ced4da;
}

.form-control {
  display: block;
  width: 100%;
  padding: 0.375rem 0.75rem;
  font-size: 1rem;
  font-weight: 400;
  line-height: 1.5;
  color: #212529;
  background-color: #fff;
  background-clip: padding-box;
  border: 1px solid #ced4da;
  border-radius: 0.25rem;
  transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
}

.form-group {
  margin-bottom: 1rem;
}

.mt-2 {
  margin-top: 0.5rem;
}

.mt-3 {
  margin-top: 1rem;
}

.mt-5 {
  margin-top: 3rem;
}

.alert-danger {
  color: #721c24;
  background-color: #f8d7da;
  border-color: #f5c6cb;
  padding: 0.75rem 1.25rem;
  border-radius: 0.25rem;
}
</style>
