```mermaid
erDiagram
    COURSE_CLASS ||--o{ STUD : ""
    TEA ||--|| COURSE_CLASS : ""
    COURSE_CLASS {
        string class_no PK "班級代碼"
        string class_name "班級名稱"
        string tea_code FK "授課教師代碼"
    }
    STUD {
        string stud_no PK "學號"
        string class_no FK "班級代碼"
        string stud_name "姓名"
    }
    TEA {
        string tea_code PK "教師代碼"
        string tea_name "教師姓名"
    }
```
