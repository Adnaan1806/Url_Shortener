# 🔗 High‑Scale URL Shortener

A distributed, scalable URL Shortener system built using modern system design principles. This project demonstrates how high‑traffic redirect platforms (like Bitly or TinyURL) are architected for performance, scalability, and reliability.

---

# 🚀 Tech Stack

* **.NET Web API** – Backend services
* **PostgreSQL** – Persistent storage
* **Redis** – In‑memory caching
* **Entity Framework Core** – ORM
* **k6** – Load testing

---

# 🏗️ System Architecture

Client → API → Redis Cache → Shard Router → PostgreSQL Shards

### Key Layers

* **API Layer** – Handles shorten & redirect requests
* **Caching Layer (Redis)** – Optimizes read performance
* **Shard Router** – Routes requests to correct database shard
* **Database Layer** – Distributed PostgreSQL storage

---

# ✨ Features

* URL shortening
* Custom short code generation
* High‑speed redirects
* Redis caching (Cache‑Aside pattern)
* Database sharding (Hash‑based)
* Horizontal scalability
* Load tested with 200 concurrent users

---

# 🧠 Scalability Design

## 🔹 Caching Strategy

**Pattern:** Cache‑Aside

Flow:

1. Check Redis
2. Cache hit → Redirect
3. Cache miss → Query shard DB
4. Store in cache

Benefits:

* Faster redirects
* Reduced DB load
* Handles hot traffic

---

## 🔹 Database Sharding

**Method:** Hash‑Based Sharding

```
hash(shortCode) % shardCount
```

Example:

| Short Code | Shard   |
| ---------- | ------- |
| abc123     | Shard 1 |
| xyz789     | Shard 2 |

Benefits:

* Even data distribution
* Horizontal scaling
* Prevents DB bottlenecks

---

# 📊 Load Testing

Tool: **k6**
Virtual Users: **200**
Duration: **30s**

## Sharding‑Only Performance

| Metric      | Value    |
| ----------- | -------- |
| Avg Latency | 1.72 s   |
| Throughput  | 87 req/s |
| Failures    | 0%       |
| Iterations  | 583      |

Result: System handled high concurrency with zero failures.

---

# 📈 Key Learnings

* Read‑heavy systems benefit most from caching
* Redis drastically improves redirect speed
* Sharding enables horizontal storage scaling
* Bottlenecks shift outward as systems scale
* Load testing validates real scalability

---

# 👥 User Benefits

* Faster redirect experience
* High availability under traffic spikes
* Scalable storage for millions of URLs
* Reliable performance

---

# 🛠️ Setup Instructions

## 1️⃣ Clone Repository

```bash
git clone https://github.com/Adnaan1806/Url_Shortener.git
cd Url_Shortener
```

---

## 2️⃣ Configure Databases

Create PostgreSQL databases:

```
url_shortener_shard1
url_shortener_shard2
```

Update connection strings in `appsettings.json`.

---

## 3️⃣ Run Migrations

```bash
dotnet ef database update
```

Run for each shard.

---

## 4️⃣ Start Redis

```bash
redis-server
```

---

## 5️⃣ Run Application

```bash
dotnet run
```

Swagger:

```
http://localhost:5116/swagger
```

---

# 🧪 Load Test

```bash
k6 run loadtest.js
```

---




