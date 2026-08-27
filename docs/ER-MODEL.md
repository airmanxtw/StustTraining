```mermaid
erDiagram
    COURSE_CLASS ||--o{ STUD : ""
    TEA ||--|| COURSE_CLASS : ""
    COURSE_CLASS {
        string class_no PK
        string class_name
        string tea_code FK
    }
    STUD {
        string stud_no PK
        string class_no FK
        string stud_name
    }
    TEA {
        string tea_code PK
        string tea_name
    }
```
