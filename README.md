# patch_downloader

# win-x86 & win-x64 msi 릴리즈 방법(Uart Driver 자동 설치 버전)
1. Patch_DataLoader 프로젝트 빌드
2. SetupDriver에서 오른쪽 마우스 클릭 후 "게시" 클릭 후 대상 런타임을 win-x64로 선택
3. 게시 버튼 클릭(자동으로 프로젝트 빌드 됨)
4. Huinno_Patch_Dataloader 프로젝트 선택 후 속성 창에서 TargetPlatform을 x64로 선택
5. 빌드를 하면 win_x64 버전 msi 설치 파일 생성됨
6. win-x86 버전 생성은 2번, 4번 항목에서 win-x86으로 선택하는 것을 제외하고 win-x64와 같음  
7. 결론적으로 Uart Driver의 정상적인 설치를 위해서 x86, x64 두 가지 버전이 릴리즈되어야 함
