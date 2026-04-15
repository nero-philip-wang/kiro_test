# AWSomeShop 设计计划

## 执行步骤

- [x] **步骤 1：澄清技术架构关键问题**
  收集用户对技术选型、架构风格、部署方式等方面的决策，确保设计方向正确。

- [x] **步骤 2：用户审查与批准计划**
  将本计划提交用户审查，获得批准后再执行后续步骤。

- [ ] **步骤 3：撰写系统架构设计（高层设计）**
  输出系统整体架构图、技术栈选型、模块划分、部署架构。

- [ ] **步骤 4：撰写数据模型设计**
  输出核心数据实体、字段定义、实体关系图（ER 图）。

- [ ] **步骤 5：撰写 API 接口设计**
  输出各模块的 RESTful API 端点定义（路径、方法、请求/响应结构）。

- [ ] **步骤 6：撰写前端页面与交互设计**
  输出员工端和管理员端的页面结构、路由规划及关键交互流程。

- [ ] **步骤 7：撰写安全与权限设计**
  输出认证机制、角色权限矩阵、审计日志设计。

- [ ] **步骤 8：用户审查设计文档**
  提交设计文档供用户审查，根据反馈进行迭代修改。

- [ ] **步骤 9：设计文档最终确认**
  用户确认设计文档无误，完成设计阶段。

---

## 待澄清问题

### 技术栈

[Question] Q1：前端技术栈是否有偏好或限制？（例如：React、Vue、Angular，或无偏好）
[Answer] Vue3

[Question] Q2：后端技术栈是否有偏好或限制？（例如：Node.js/Express、Python/FastAPI、Java/Spring Boot、Go，或无偏好）
[Answer] ASP.NET 10

[Question] Q3：数据库是否有偏好？（例如：PostgreSQL、MySQL、MongoDB，或无偏好）
[Answer] SQLite

[Question] Q4：是否需要独立的缓存层？（例如：Redis 用于 Session 管理或热点数据缓存）
[Answer] 不需要

---

### 部署与基础设施

[Question] Q5：系统部署在哪里？（例如：AWS 云服务、公司内网服务器、Docker 容器、Kubernetes，或无要求）
[Answer] Docker 容器

[Question] Q6：是否需要考虑 CI/CD 流水线？（例如：GitHub Actions、Jenkins，或 MVP 阶段暂不需要）
[Answer] CI/CD 流水线, GitHub Actions

[Question] Q7：图片（商品图片）存储方案是什么？（例如：AWS S3、本地文件系统、CDN，或无偏好）
[Answer] 本地文件系统

---

### 认证与安全

[Question] Q8：用户认证方案偏好是什么？（例如：JWT Token、Session Cookie，或无偏好）
[Answer] JWT Token

[Question] Q9：密码重置链接的有效期是多少？（例如：1 小时、24 小时）是否有要求？
[Answer] 24 小时

[Question] Q10：是否需要登录失败次数限制（防暴力破解）？例如：连续失败 5 次后锁定账号 N 分钟。
[Answer]  5 次 10 分钟

---

### 邮件服务

[Question] Q11：发送初始密码和密码重置邮件使用什么邮件服务？（例如：AWS SES、SendGrid、公司 SMTP 服务器，或无偏好）
[Answer] 公司 SMTP 服务器

---

### 前端架构

[Question] Q12：员工端和管理员端是否作为同一个前端项目（通过路由区分），还是两个独立的前端应用？
[Answer] 通过路由区分

[Question] Q13：国际化（i18n）方案是否有偏好？（例如：前端框架内置 i18n、react-i18next、vue-i18n，或无偏好）
[Answer] vue-i18n

---

### 文件导出

[Question] Q14：Excel 导出是在前端生成（如 SheetJS）还是由后端生成后下载？
[Answer] 后端生成

---

### 积分有效期实现

[Question] Q15：积分过期检查是通过定时任务（如每天凌晨批量标记）实现，还是在每次查询时实时计算？两种方式各有取舍，您是否有偏好？
[Answer] 定时任务

---

## 追加问题

### ASP.NET 10 后端架构（基于 Q2 回答）

[Question] Q16：后端 API 架构风格偏好是什么？（例如：单体 MVC 架构、Clean Architecture 分层架构、最小 API（Minimal API），或无偏好）
[Answer] REST API, 采用DDD风格

[Question] Q17：前后端是否完全分离（前端 Vue3 SPA + 后端纯 REST API），还是部分使用服务端渲染？
[Answer] 完全分离

---

### Docker 部署（基于 Q5 回答）

[Question] Q18：Docker 部署是否使用 docker-compose 编排多个容器（如前端 Nginx + 后端 API + SQLite），还是打包为单一容器？
[Answer] 打包为单一容器

---

### 图片存储（基于 Q7 回答）

[Question] Q19：商品图片存储在本地文件系统时，是存储在后端服务器的某个目录下并通过 API 提供访问，还是通过 Nginx 直接提供静态文件服务？图片是否需要限制上传大小或格式（如仅支持 JPG/PNG，最大 2MB）？
[Answer] 存储在后端服务器的upload目录, 上传大小10M

---

### JWT 认证（基于 Q8 回答）

[Question] Q20：JWT Token 的有效期设置是多少？（例如：Access Token 2 小时 + Refresh Token 7 天，或其他方案）是否需要 Refresh Token 机制？
[Answer] Access Token 2 小时 + Refresh Token 30 天

---

### GitHub Actions CI/CD（基于 Q6 回答）

[Question] Q21：GitHub Actions 流水线需要包含哪些阶段？（例如：代码检查 Lint → 单元测试 → 构建 Docker 镜像 → 推送镜像仓库 → 自动部署）MVP 阶段最低需要哪些？
[Answer] 代码检查 Lint → 单元测试 → 构建 Docker 镜像 → 推送镜像仓库 → 自动部署

---

### SQLite 与并发（基于 Q3 回答）

[Question] Q22：SQLite 在并发写入时有限制，考虑到用户规模 100 人以下，这在 MVP 阶段是可接受的。请确认：未来是否有可能迁移到 PostgreSQL 或 MySQL？如果有，设计时是否需要考虑数据库无关性（如使用 ORM 抽象层）？
[Answer] 使用EF 作为ORM, 后期可能考虑迁移 PostgreSQL

---

## 备注

- 所有问题回答完毕后，请通知我进行审查和批准，然后将进入步骤 3 开始撰写设计文档。
- 如有新的疑问，将在本文件末尾追加问题。
